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

namespace Boulderlog.Controllers
{
    public class SessionFiltersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SessionFiltersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SessionFilters
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.SessionFilter.Include(s => s.Franchise).Include(s => s.Grade).Include(s => s.Gym).Include(s => s.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: SessionFilters/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sessionFilter = await _context.SessionFilter
                .Include(s => s.Franchise)
                .Include(s => s.Grade)
                .Include(s => s.Gym)
                .Include(s => s.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sessionFilter == null)
            {
                return NotFound();
            }

            return View(sessionFilter);
        }

        // GET: SessionFilters/Create
        public IActionResult Create()
        {
            ViewData["FranchiseId"] = new SelectList(_context.Franchise, "Id", "Id");
            ViewData["GradeId"] = new SelectList(_context.Grade, "Id", "Id");
            ViewData["GymId"] = new SelectList(_context.Gym, "Id", "Id");
            return View();
        }

        // POST: SessionFilters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,GymId,FranchiseId,GradeId,HoldColor,Wall,LastUpdated")] SessionFilter sessionFilter)
        {
            sessionFilter.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (ModelState.IsValid)
            {
                _context.Add(sessionFilter);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FranchiseId"] = new SelectList(_context.Franchise, "Id", "Id", sessionFilter.FranchiseId);
            ViewData["GradeId"] = new SelectList(_context.Grade, "Id", "Id", sessionFilter.GradeId);
            ViewData["GymId"] = new SelectList(_context.Gym, "Id", "Id", sessionFilter.GymId);
            return View(sessionFilter);
        }

        // GET: SessionFilters/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sessionFilter = await _context.SessionFilter.FindAsync(id);
            if (sessionFilter == null)
            {
                return NotFound();
            }
            ViewData["FranchiseId"] = new SelectList(_context.Franchise, "Id", "Id", sessionFilter.FranchiseId);
            ViewData["GradeId"] = new SelectList(_context.Grade, "Id", "Id", sessionFilter.GradeId);
            ViewData["GymId"] = new SelectList(_context.Gym, "Id", "Id", sessionFilter.GymId);
            return View(sessionFilter);
        }

        // POST: SessionFilters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("Id,UserId,GymId,FranchiseId,GradeId,HoldColor,Wall,LastUpdated")] SessionFilter sessionFilter)
        {
            if (id != sessionFilter.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sessionFilter);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SessionFilterExists(sessionFilter.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["FranchiseId"] = new SelectList(_context.Franchise, "Id", "Id", sessionFilter.FranchiseId);
            ViewData["GradeId"] = new SelectList(_context.Grade, "Id", "Id", sessionFilter.GradeId);
            ViewData["GymId"] = new SelectList(_context.Gym, "Id", "Id", sessionFilter.GymId);
            return View(sessionFilter);
        }

        // GET: SessionFilters/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sessionFilter = await _context.SessionFilter
                .Include(s => s.Franchise)
                .Include(s => s.Grade)
                .Include(s => s.Gym)
                .Include(s => s.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sessionFilter == null)
            {
                return NotFound();
            }

            return View(sessionFilter);
        }

        // POST: SessionFilters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var sessionFilter = await _context.SessionFilter.FindAsync(id);
            if (sessionFilter != null)
            {
                _context.SessionFilter.Remove(sessionFilter);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SessionFilterExists(int? id)
        {
            return _context.SessionFilter.Any(e => e.Id == id);
        }
    }
}
