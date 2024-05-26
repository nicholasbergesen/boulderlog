using Boulderlog.Data;
using Boulderlog.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Boulderlog.Controllers
{
    [Authorize]
    public class ClimbController : Controller
    {
        private static IEnumerable<string> GradeSelect = new List<string>() { string.Empty, "White", "Yellow", "Orange", "Green", "Blue", "Red", "Purple", "Grey", "Brown", "Black" };
        private static IEnumerable<string> GymSelect = new List<string>() { string.Empty, "TheClimb-Yeonnam" };
        private static IEnumerable<string> HoldColor = new List<string>() { string.Empty, "White", "Yellow", "Orange", "Green", "Blue", "Red", "Purple", "Grey", "Brown", "Black", "Pink" };
        private static IEnumerable<string> Wall = new List<string>() { string.Empty, "Yeonnam", "Toitmaru", "Sinchon" };
        private readonly ApplicationDbContext _context;

        public ClimbController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Climb
        public async Task<IActionResult> Index(string grade, string gym)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var climbs = _context.Climb
                .Include(c => c.User)
                .Include(c => c.ClimbLogs)
                .Where(c => c.UserId == userId);

            if (!string.IsNullOrEmpty(grade))
            {
                climbs = climbs.Where(x => grade.Equals(x.Grade));
            }

            if (!string.IsNullOrEmpty(gym))
            {
                climbs = climbs.Where(x => gym.Equals(x.Gym));
            }

            foreach (var climb in climbs)
            {
                var attempts = climb.ClimbLogs.GroupBy(x => $"{x.ClimbId}-{x.Type}");
                foreach (var attempt in attempts)
                {
                    ViewData[attempt.Key] = attempt.Count();
                }
            }
            ViewData["Grade"] = new SelectList(GradeSelect, grade);
            ViewData["Gym"] = new SelectList(GymSelect, gym);

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
            ViewData["Gym"] = new SelectList(GymSelect, "TheClimb-Yeonnam");
            ViewData["Grade"] = new SelectList(GradeSelect, "Red");
            ViewData["HoldColor"] = new SelectList(HoldColor, "Red");
            ViewData["Wall"] = new SelectList(Wall, "Yeonnam");
            ViewData["UserId"] = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return View();
        }

        // POST: Climb/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ImageId,Grade,HoldColor,Gym,Wall,UserId")] Climb climb)
        {
            ViewData["Gym"] = new SelectList(GymSelect, climb.Gym);
            ViewData["Grade"] = new SelectList(GradeSelect, climb.Grade);
            ViewData["HoldColor"] = new SelectList(HoldColor, climb.HoldColor);
            ViewData["Wall"] = new SelectList(Wall, climb.Wall);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", climb.UserId);

            var userId = User.FindFirst(ClaimTypes.NameIdentifier);

            if (climb.UserId != userId?.Value)
            {
                ModelState.AddModelError("UserId", "UserId is invalid");
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

            ViewData["Gym"] = new SelectList(GymSelect, climb.Gym);
            ViewData["Grade"] = new SelectList(GradeSelect, climb.Grade);
            ViewData["HoldColor"] = new SelectList(HoldColor, climb.HoldColor);
            ViewData["Wall"] = new SelectList(Wall, climb.Wall);
            return View(climb);
        }

        // POST: Climb/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,ImageId,Grade,HoldColor,Gym,Wall,UserId")] Climb climb)
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
