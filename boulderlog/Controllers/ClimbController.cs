﻿using Boulderlog.Data;
using Boulderlog.Data.Models;
using Boulderlog.Domain;
using Boulderlog.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor.Compilation;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Boulderlog.Controllers
{
    [Authorize]
    public class ClimbController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClimbController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Climb
        [ResponseCache(Location = ResponseCacheLocation.Client, Duration = 30, VaryByQueryKeys = ["gymId", "gradeId", "wall"])]
        public IActionResult MyClimbs(int? gymId, int gradeId, string wall)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var climbLogs = _context.ClimbLog
                .Include(c => c.Climb)
                .Include(c => c.Climb.Gym)
                .Include(c => c.Climb.Grade)
                .Include(c => c.Climb.Franchise)
                .Where(c => c.UserId == userId);

            if (gradeId < 1 || !gymId.HasValue)
            {
                var timeZone = TimeZoneInfo.FindSystemTimeZoneById("Korea Standard Time");
                var koreaTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZone);
                koreaTime = koreaTime.AddDays(-30);
                climbLogs = climbLogs.Where(x => x.TimeStamp > koreaTime);
            }

            var climbs = climbLogs
                .Select(x => x.Climb)
                .Distinct();

            if (gymId.HasValue)
            {
                climbs = climbs.Where(x => gymId.Equals(x.GymId));
            }

            if (gradeId > 0)
            {
                climbs = climbs.Where(x => gradeId.Equals(x.GradeId));
            }

            if (!string.IsNullOrEmpty(wall))
            {
                climbs = climbs.Where(x => wall.Equals(x.Wall));
            }

            climbs = climbs.OrderByDescending(x => x.ClimbLogs.Max(x => x.TimeStamp));

            List <ClimbViewModel> climbViewModels = new List<ClimbViewModel>();

            foreach (var climb in climbs)
            {
                var logsForClimb = climbLogs.Where(x => x.ClimbId == climb.Id);
                var climbModel = new ClimbViewModel()
                {
                    Id = climb.Id,
                    Gym = climb.Gym.Name,
                    Grade = climb.Grade.ColorName,
                    GradeColor = climb.Grade.ColorHex,
                    ImageId = climb.ImageId,
                    HoldColor = climb.HoldColor,
                    Wall = climb.Wall,
                    UserId = userId,
                    IsFlashed = "Top" == logsForClimb.OrderBy(x => x.TimeStamp).FirstOrDefault()?.Type
                };

                var attempts = logsForClimb
                    .GroupBy(x => x.Type);

                foreach (var attempt in attempts)
                {
                    switch (attempt.Key)
                    {
                        case "Attempt":
                            climbModel.Attempt = attempt.Count();
                            break;
                        case "Top":
                            climbModel.Top = attempt.Count();
                            break;
                        default:
                            throw new Exception("Unhandled ClimbLog Type");
                    }
                }
                climbViewModels.Add(climbModel);
            }

            var gyms = _context.Gym.Include(x => x.Franchise).Select(x => new { x.Id, x.Name, Group = new SelectListGroup { Name = x.Franchise.Name } });
            var pageModel = new ClimbPageViewModel();
            pageModel.Gyms = new SelectList(gyms, "Id", "Name", null, "Group.Name");
            pageModel.ClimbViewModels = climbViewModels;
            pageModel.SelectedGymId = gymId ?? 2;

            return View(pageModel);
        }

        [HttpPost]
        public IActionResult Session(int? gymId)
        {
            var climbs = _context.Climb.Where(x => x.GymId == gymId);
            return View();
        }

        public IActionResult Session(int? gymId, int gradeId, string wall)
        {
            var climbs = _context.Climb.Where(x => x.GymId == gymId);
            return View();
        }

        // GET: Climb/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var climb = await _context.Climb
                .Include(c => c.CreatedByUser)
                .Include(c => c.ClimbLogs)
                .Include(x => x.Grade)
                .Include(x => x.Gym)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (climb == null)
            {
                return NotFound();
            }

            var climbModel = new ClimbViewModel()
            {
                Id = climb.Id,
                Gym = climb.Gym.Name,
                Grade = climb.Grade.ColorName,
                GradeColor = climb.Grade.ColorHex,
                ImageId = climb.ImageId,
                HoldColor = climb.HoldColor,
                Wall = climb.Wall,
                UserId = climb.CreatedByUserId,
                ClimbLogs = climb.ClimbLogs
            };

            return View(climbModel);
        }

        // GET: Climb/Create
        public IActionResult Create(int? gymId)
        {
            var gyms = _context.Gym.Include(x => x.Franchise).Select(x => new { x.Id, x.Name, Group = new SelectListGroup { Name = x.Franchise.Name } });
            ViewData["Gym"] = new SelectList(gyms, "Id", "Name", null, "Group.Name");
            ViewData["HoldColor"] = new SelectList(Const.HoldColors);
            var model = new Climb();
            model.GymId = gymId ?? 2;
            model.CreatedByUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return View(model);
        }

        // POST: Climb/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ImageId,GymId,Wall,GradeId,HoldColor,CreatedByUserId,TimeStamp")] Climb climb)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier);

            if (climb.CreatedByUserId != userId?.Value)
            {
                ModelState.AddModelError("UserId", "UserId is invalid");
            }

            if (ModelState.IsValid)
            {
                var gym = await _context.Gym.FindAsync(climb.GymId);
                climb.FranchiseId = gym.FranchiseId;

                _context.Add(climb);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(MyClimbs));
            }

            var gyms = _context.Gym.Select(x => new { x.Id, x.Name, x.Walls });
            var grade = _context.Grade.Where(x => x.Id == climb.GymId).Select(x => new { x.Id, x.ColorName });
            ViewData["Gym"] = new SelectList(gyms, "Id", "Name", climb.GymId);
            ViewData["Grade"] = new SelectList(grade, "Id", "ColorName", climb.GradeId);
            ViewData["Wall"] = new SelectList(gyms.First(x => x.Id == climb.GymId).Walls.Split(";"), climb.Wall);
            ViewData["HoldColor"] = new SelectList(Const.HoldColors, climb.HoldColor);
            return View(climb);
        }

        // GET: Climb/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var climb = await _context.Climb
                .Include(x => x.Gym)
                .Include(x => x.Franchise)
                .Include(x => x.Franchise.Grade)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (climb == null)
            {
                return NotFound();
            }

            var gyms = _context.Gym.Include(x => x.Franchise).Select(x => new { x.Id, x.Name, Group = new SelectListGroup { Name = x.Franchise.Name } });
            var grade = climb.Franchise.Grade.Select(x => new { x.Id, x.ColorName });
            ViewData["Gym"] = new SelectList(gyms, "Id", "Name", null, "Group.Name");
            ViewData["Grade"] = new SelectList(grade, "Id", "ColorName", climb.GradeId);
            ViewData["Wall"] = new SelectList(climb.Gym.Walls.Split(";"), climb.Wall);
            ViewData["HoldColor"] = new SelectList(Const.HoldColors, climb.HoldColor);
            return View(climb);
        }

        // POST: Climb/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,ImageId,GymId,Wall,GradeId,HoldColor,CreatedByUserId,GradeOld,GymOld")] Climb climb)
        {
            if (id != climb.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var gym = await _context.Gym.FindAsync(climb.GymId);
                    climb.FranchiseId = gym.FranchiseId;

                    _context.Update(climb);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClimbExists(climb.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(MyClimbs));
            }

            var gyms = _context.Gym.Select(x => new { x.Id, x.Name, x.Walls });
            var grade = _context.Grade.Where(x => x.Id == climb.GymId).Select(x => new { x.Id, x.ColorName });
            ViewData["Gym"] = new SelectList(gyms, "Id", "Name", climb.GymId);
            ViewData["Grade"] = new SelectList(grade, "Id", "ColorName", climb.GradeId);
            ViewData["Wall"] = new SelectList(gyms.First(x => x.Id == climb.GymId).Walls.Split(";"), climb.Wall);
            ViewData["HoldColor"] = new SelectList(Const.HoldColors, climb.HoldColor);
            return View(climb);
        }

        // GET: Climb/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var climb = await _context.Climb
                .Include(c => c.CreatedByUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (climb == null)
            {
                return NotFound();
            }

            return View(climb);
        }

        // POST: Climb/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var climb = await _context.Climb
                .Include(x => x.ClimbLogs)
                .FirstOrDefaultAsync(x=> id.Equals(x.Id));

            if (climb != null)
            {
                var image = await _context.Image.FindAsync(climb.ImageId);
                if (image != null)
                {
                    _context.Image.Remove(image);
                }

                _context.ClimbLog.RemoveRange(climb.ClimbLogs);
                _context.Climb.Remove(climb);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(MyClimbs));
        }

        private bool ClimbExists(string id)
        {
            return _context.Climb.Any(e => e.Id == id);
        }
    }
}
