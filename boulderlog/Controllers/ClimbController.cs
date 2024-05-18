using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using boulderlog.Data;
using boulderlog.Data.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace boulderlog.Controllers
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
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var climbs = _context.Climb
                .Include(c => c.User)
                .Include(c => c.ClimbLogs)
                .Where(c => c.UserId == userId);

            foreach(var climb in climbs)
            {
                var attempts = climb.ClimbLogs.GroupBy(x => x.Type);
                foreach(var attempt in attempts)
                {
                    ViewData[attempt.Key] = attempt.Count();
                }
            }

            return View(await climbs.ToListAsync());
        }

        // GET: Climb/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var climb = await _context.Climb
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (climb == null)
            {
                return NotFound();
            }

            return View(climb);
        }

        // GET: Climb/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return View();
        }

        // POST: Climb/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Color,Gym,Wall,UserId")] Climb climb)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier);

            if (climb.UserId != userId?.Value)
            {
                ModelState.AddModelError("Invalid", "Invalid request");
            }

            if (ModelState.IsValid)
            {
                _context.Add(climb);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(climb);
        }

        // GET: Climb/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var climb = await _context.Climb.FindAsync(id);
            if (climb == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", climb.UserId);
            return View(climb);
        }

        // POST: Climb/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Name,Color,Gym,Wall,UserId")] Climb climb)
        {
            if (id != climb.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
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
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", climb.UserId);
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
                .Include(c => c.User)
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
            var climb = await _context.Climb.FindAsync(id);
            if (climb != null)
            {
                _context.Climb.Remove(climb);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClimbExists(string id)
        {
            return _context.Climb.Any(e => e.Id == id);
        }
    }
}
