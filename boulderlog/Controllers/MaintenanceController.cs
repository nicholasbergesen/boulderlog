﻿using Boulderlog.Data;
using Boulderlog.Data.Models;
using Boulderlog.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Options;
using NuGet.Protocol;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Boulderlog.Domain.Const;

namespace Boulderlog.Controllers
{
    [Route("[controller]")]
    public class MaintenanceController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly AppConfigOptions _appConfigOptions;

        public MaintenanceController(ApplicationDbContext context, UserManager<AppUser> userManager, IOptions<AppConfigOptions> appConfigOptions)
        {
            _context = context;
            _userManager = userManager;
            _appConfigOptions = appConfigOptions.Value;
        }

        [Route("Migration")]
        public async Task<IActionResult> Migration()
        {
            await _context.Database.MigrateAsync();
            await _context.SaveChangesAsync();

            CreateRoleIfNotExist(Role.Admin);
            CreateRoleIfNotExist(Role.User);
            if (!string.IsNullOrEmpty(_appConfigOptions.AdminUserEmail))
            {
                var user = await _userManager.FindByEmailAsync("nicholasb.za@gmail.com");

                // Assign admin role if user with email exists.
                if (user != null)
                {
                    await _context.SaveChangesAsync();

                    var userRole = _context.UserRoles.SingleOrDefault(x => x.UserId == user.Id);
                    if (userRole == null)
                    {
                        var adminRole = _context.Roles.FirstOrDefault(x => Role.Admin.Equals(x.Name));
                        if (adminRole != null)
                        {
                            _context.UserRoles.Add(new IdentityUserRole<string>()
                            {
                                UserId = user.Id,
                                RoleId = adminRole.Id
                            });
                        }
                    }
                }
            }

            await _context.SaveChangesAsync();
            return Ok();
        }

        private void CreateRoleIfNotExist(string role)
        {
            var dbRole = _context.Roles.FirstOrDefault(x => role.Equals(x.Name));
            if (dbRole == null)
            {
                _context.Roles.Add(new AppRole(role));
            }
        }

        [Route("CleanDatabase")]
        public async Task<IActionResult> CleanDatabase()
        {
            var imagesIds = _context.Climb.Select(x => x.ImageId).ToList();
            var orphanedImages = _context.Image.Where(x => !imagesIds.Contains(x.Id)).ToList();
            _context.Image.RemoveRange(orphanedImages);
            await _context.SaveChangesAsync();

            foreach (var climbLogItem in _context.ClimbLog)
            {
                var climb = await _context.Climb.FindAsync(climbLogItem.ClimbId);
                if (climb == null)
                {
                    _context.ClimbLog.Remove(climbLogItem);
                }
            }

            var num = await _context.SaveChangesAsync();

            return Ok(num);
        }

        [Route("SeedDatabase")]
        public async Task<IActionResult> SeedDatabase()
        {
            Dictionary<string, int> grades = new Dictionary<string, int>()
            {
                 {"1",1},
                 {"2",2},
                 {"3",3},
                 {"4",4},
                 {"5",5},
                 {"6",6},
                 {"7",7},
                 {"8",8},
                 {"9",9 },
                 {"White",10},
                 {"Yellow",11},
                 {"Orange",12},
                 {"Green",13},
                 {"Blue", 14},
                 {"Red", 15},
                 {"Purple",16},
                 {"Grey", 17},
                 {"Brown",18},
                 {"Black",19}
            };

            var climbs = _context.Climb;
            foreach (var climb in climbs)
            {
                if (climb.Gym == "TheClimb-Yeonnam")
                {
                    climb.GymId = 2;
                }
                else
                {
                    climb.GymId = 1;
                }

                climb.GradeId = grades[climb.Grade];
            }

            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
