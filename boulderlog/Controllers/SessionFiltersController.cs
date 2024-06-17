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
    public class SessionFiltersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SessionFiltersController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = Const.Role.Admin)]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.SessionFilter.Include(s => s.Franchise).Include(s => s.Grade).Include(s => s.Gym).Include(s => s.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: SessionFilters/Create
        public async Task<IActionResult> Create()
        {
            ViewData["FranchiseId"] = new SelectList(_context.Franchise, "Id", "Id");
            ViewData["GradeId"] = new SelectList(_context.Grade, "Id", "Id");
            ViewData["GymId"] = new SelectList(_context.Gym, "Id", "Id");
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var sessionFilter = await _context.SessionFilter.FirstOrDefaultAsync(x => x.UserId == userId);

            if (sessionFilter != null)
            {
                return View(sessionFilter);
            }

            var model = new SessionFilter()
            {
                UserId = User.FindFirstValue(ClaimTypes.NameIdentifier)
            };
            return View(model);
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
