using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Boulderlog.Data;
using Boulderlog.Data.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Boulderlog.Domain;

namespace Boulderlog.Controllers
{
    [Authorize]
    public class SessionFilterController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SessionFilterController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = Const.Role.Admin)]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.SessionFilter.Include(s => s.Franchise).Include(s => s.Grade).Include(s => s.Gym).Include(s => s.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // POST: SessionFilters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,GymId,UserId,FranchiseId,GradeId,HoldColor,Wall")] SessionFilter sessionFilter)
        {
            if (ModelState.IsValid)
            {
                if (sessionFilter.Id != null)
                {
                    _context.Update(sessionFilter);
                }
                else
                {
                    _context.Add(sessionFilter);
                }

                await _context.SaveChangesAsync();
            }

            ViewData["FranchiseId"] = new SelectList(_context.Franchise, "Id", "Id", sessionFilter.FranchiseId);
            ViewData["GradeId"] = new SelectList(_context.Grade, "Id", "Id", sessionFilter.GradeId);
            ViewData["GymId"] = new SelectList(_context.Gym, "Id", "Id", sessionFilter.GymId);

            return Ok();
        }
    }
}
