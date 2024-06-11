using Boulderlog.Data;
using Boulderlog.Data.Models;
using Boulderlog.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    public class ClimbLogController : Controller
    {
        private readonly ApplicationDbContext _context;
        private static IEnumerable<string> Type = new List<string>() { "Attempt", "Top" };

        public ClimbLogController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ClimbLog
        public async Task<IActionResult> Index(int? gymId)
        {
            if (gymId is null)
            {
                gymId = 2;
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var thirtyDaysAgo = DateTime.UtcNow.AddDays(-30);

            var climbLogs = await _context
                .ClimbLog
                .Include(c => c.Climb)
                .Where(x => x.Climb.UserId == userId)
                .Where(x => x.TimeStamp > thirtyDaysAgo)
                .OrderBy(x => x.TimeStamp)
                .ToListAsync();

            var climbsPerDay = climbLogs.GroupBy(x => $"{x.TimeStamp.Year}-{x.TimeStamp.Month}-{x.TimeStamp.Day}", x => new { x.Type, x.ClimbId });
            var model = new ClimbLogViewModel();
            foreach (var climbs in climbsPerDay)
            {
                model.SessionLabels.Add($"{climbs.Key}");
                model.SessionValuesAttempt.Add(climbs.Count(x => x.Type == "Attempt"));
                model.SessionValuesTop.Add(climbs.Count(x => x.Type == "Top"));
                model.SessionBoulders.Add(climbs.Select(x => x.ClimbId).Distinct().Count());
            }

            if (gymId > 0)
            {
                var grades = _context.Grade.Where(x => x.GymId == gymId);
                foreach (var grade in grades)
                {
                    var logsForGrade = climbLogs.Where(x => grade.Id.Equals(x.Climb.GradeId));

                    if (logsForGrade.Count() == 0)
                    {
                        continue;
                    }

                    // Sucecss rate
                    model.GradeSuccessRate_Values.Add(1.0 * logsForGrade.Count(x => x.Type == "Top") / logsForGrade.Count());
                    model.GradeSuccessRate_Label.Add(grade.ColorName);

                    // Averate Attempts
                    model.GradeAverageAttempt_Values.Add(1.0 * logsForGrade.Count() / logsForGrade.DistinctBy(x => x.ClimbId).Count());
                    model.GradeAverageAttempt_Label.Add(grade.ColorName);
                }
            }

            var gyms = _context.Gym.Select(x => new { x.Id, x.Name });
            model.Gyms = new SelectList(gyms, "Id", "Name", gymId);

            return View(model);
        }

        // GET: ClimbLog/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var climbLog = await _context.ClimbLog
                .Include(c => c.Climb)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (climbLog == null)
            {
                return NotFound();
            }

            return View(climbLog);
        }

        // GET: ClimbLog/Create
        public IActionResult Create(string climbId)
        {
            ViewData["Type"] = new SelectList(Type, "Attempt");
            ViewBag.ClimbId = climbId;
            return View(new ClimbLog() { ClimbId = climbId, Type = "Attempt" });
        }

        // POST: ClimbLog/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TimeStamp,Type,ClimbId")] ClimbLog climbLog)
        {
            if (ModelState.IsValid)
            {
                _context.Add(climbLog);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Climb");
            }

            ViewData["ClimbId"] = new SelectList(_context.Climb, "Id", "Id", climbLog.ClimbId);
            return View(climbLog);
        }

        // GET: ClimbLog/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var climbLog = await _context.ClimbLog.FindAsync(id);
            if (climbLog == null)
            {
                return NotFound();
            }
            ViewData["ClimbId"] = new SelectList(_context.Climb, "Id", "Id", climbLog.ClimbId);
            return View(climbLog);
        }

        // POST: ClimbLog/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("TimeStamp,Type,ClimbId")] ClimbLog climbLog)
        {
            if (id != climbLog.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(climbLog);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClimbLogExists(climbLog.Id))
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
            ViewData["ClimbId"] = new SelectList(_context.Climb, "Id", "Id", climbLog.ClimbId);
            return View(climbLog);
        }

        // GET: ClimbLog/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var climbLog = await _context.ClimbLog
                .Include(c => c.Climb)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (climbLog == null)
            {
                return NotFound();
            }

            return View(climbLog);
        }

        // POST: ClimbLog/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var climbLog = await _context.ClimbLog.FindAsync(id);
            if (climbLog != null)
            {
                _context.ClimbLog.Remove(climbLog);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClimbLogExists(string id)
        {
            return _context.ClimbLog.Any(e => e.Id == id);
        }
    }
}
