using Boulderlog.Data;
using Boulderlog.Data.Models;
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
        private static IEnumerable<string> Grades = new List<string>() { "White", "Yellow", "Orange", "Green", "Blue", "Red", "Purple", "Grey", "Brown", "Black" };
        private static IEnumerable<string> GymSelect = new List<string>() { string.Empty, "TheClimb-Yeonnam", "TheClimb-B-Hongdae" };
        private static IEnumerable<string> HoldColor = new List<string>() { string.Empty, "White", "Yellow", "Orange", "Green", "Blue", "Red", "Purple", "Grey", "Brown", "Black", "Pink" };
        private static IEnumerable<string> Wall = new List<string>() { string.Empty, "Yeonnam", "Toitmaru", "Sinchon" };
        private static IEnumerable<string> Type = new List<string>() { "Attempt", "Top", "Flash" };

        public ClimbLogController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ClimbLog
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var thirtyDaysAgo = DateTime.UtcNow.AddDays(-30);

            var climbLogs = await _context
                .ClimbLog
                .Include(c => c.Climb)
                .Where(x => x.Climb.UserId == userId)
                .Where(x => x.TimeStamp > thirtyDaysAgo)
                .OrderBy(x => x.TimeStamp)
                .ToListAsync();

            var gradeCount = new List<int>();
            foreach (var grade in Grades)
            {
                gradeCount.Add(climbLogs.Count(x => grade.Equals(x.Climb.GradeOld)));
            }

            ViewData["GradeLabels"] = Grades;
            ViewData["GradeValues"] = gradeCount;

            var climbsPerDay = climbLogs.GroupBy(x => $"{x.TimeStamp.Year}-{x.TimeStamp.Month}-{x.TimeStamp.Date}", x => x.Type);
            List<string> sessionLabels = new List<string>();
            List<int> sessionValuesAttempt = new List<int>();
            List<int> sessionValuesTop = new List<int>();
            List<int> sessionValuesFlash = new List<int>();
            foreach (var climbs in climbsPerDay)
            {
                sessionLabels.Add($"{climbs.Key}");
                sessionValuesAttempt.Add(climbs.Count(x => x == "Attempt"));
                sessionValuesTop.Add(climbs.Count(x => x == "Top"));
                sessionValuesFlash.Add(climbs.Count(x => x == "Flash"));
            }

            ViewData["SessionLabels"] = sessionLabels;
            ViewData["SessionAttempt"] = sessionValuesAttempt;
            ViewData["SessionTop"] = sessionValuesTop;
            ViewData["SessionFlash"] = sessionValuesFlash;

            return View();
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
