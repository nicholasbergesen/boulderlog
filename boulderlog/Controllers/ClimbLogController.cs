using Boulderlog.Data;
using Boulderlog.Data.Models;
using Boulderlog.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
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
        public async Task<IActionResult> Index()
        {
            var franchises = _context.Franchise.Select(x => new { x.Id, x.Name });

            var model = new ClimbLogViewModel()
            {
                FranchiseId = 2,
                From = DateTime.Now.AddDays(-30).Date,
                To = DateTime.Now.Date,
                Franchises = new SelectList(franchises, "Id", "Name", null),
            };

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var climbLogs = await _context
                .ClimbLog
                .Include(c => c.Climb)
                .Where(x => x.UserId == userId)
                .Where(x => model.From <= x.TimeStamp && x.TimeStamp <= model.To)
                .OrderBy(x => x.TimeStamp)
                .ToListAsync();

            var climbsPerDay = climbLogs.GroupBy(x => $"{x.TimeStamp.Year}-{x.TimeStamp.Month}-{x.TimeStamp.Day}", x => new { x.Type, x.ClimbId });
            foreach (var climbs in climbsPerDay)
            {
                model.SessionLabels.Add($"{climbs.Key}");
                model.SessionValuesAttempt.Add(climbs.Count(x => x.Type == "Attempt"));
                model.SessionValuesTop.Add(climbs.Count(x => x.Type == "Top"));
                model.SessionBoulders.Add(climbs.Select(x => x.ClimbId).Distinct().Count());
            }

            var grades = (await _context.Franchise.Include(x => x.Grade).FirstOrDefaultAsync(x => x.Id == model.FranchiseId)).Grade;
            foreach (var grade in grades)
            {
                var logsForGrade = climbLogs.Where(x => grade.Id.Equals(x.Climb.GradeId));

                if (logsForGrade.Count() == 0)
                {
                    continue;
                }

                var tops = logsForGrade.Count(x => x.Type == "Top");
                var attempt = logsForGrade.Count(x => x.Type == "Attempt");
                var uniqueClimbs = logsForGrade.DistinctBy(x => x.ClimbId).Count();
                var totalClimbs = logsForGrade.Count();

                // Sucecss rate
                model.GradeSuccessRate_Values.Add(Math.Round(1.0 * tops / totalClimbs * 100, 2));
                model.GradeSuccessRate_Label.Add(grade.ColorName);

                // Attempt:Top ratio
                model.GradeRatioAttempt_Values.Add(Math.Round(1.0 * attempt / totalClimbs, 2));
                model.GradeRatioTop_Values.Add(Math.Round(1.0 * tops / totalClimbs, 2));
                model.GradeRatioAttempt_Label.Add(grade.ColorName);

                // Average attempts
                model.GradeAverageAttempt_Values.Add(Math.Round(1.0 * totalClimbs / uniqueClimbs, 2));
                model.GradeAverageAttempt_Label.Add(grade.ColorName);

                // Untopped
                var climbsWithoutTops = logsForGrade.GroupBy(x => x.ClimbId, x => x.Type).Count(x => !x.Any(x => x == "Top"));
                model.Untopped_Values.Add(climbsWithoutTops);
                model.Untopped_Label.Add(grade.ColorName);
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(int? franchiseId, DateTime? from, DateTime? to)
        {
            var franchises = _context.Franchise.Select(x => new { x.Id, x.Name });

            var model = new ClimbLogViewModel()
            {
                FranchiseId = franchiseId ?? 2,
                From = from.GetValueOrDefault(DateTime.Now.AddDays(-30)).Date,
                To = to.GetValueOrDefault(DateTime.Now).Date,
                Franchises = new SelectList(franchises, "Id", "Name", null),
            };

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var climbLogs = await _context
                .ClimbLog
                .Include(c => c.Climb)
                .Where(x => x.UserId == userId)
                .Where(x => model.From <= x.TimeStamp && x.TimeStamp <= model.To)
                .OrderBy(x => x.TimeStamp)
                .ToListAsync();

            var climbsPerDay = climbLogs.GroupBy(x => $"{x.TimeStamp.Year}-{x.TimeStamp.Month}-{x.TimeStamp.Day}", x => new { x.Type, x.ClimbId });
            foreach (var climbs in climbsPerDay)
            {
                model.SessionLabels.Add($"{climbs.Key}");
                model.SessionValuesAttempt.Add(climbs.Count(x => x.Type == "Attempt"));
                model.SessionValuesTop.Add(climbs.Count(x => x.Type == "Top"));
                model.SessionBoulders.Add(climbs.Select(x => x.ClimbId).Distinct().Count());
            }

            var grades = (await _context.Franchise.Include(x => x.Grade).FirstOrDefaultAsync(x => x.Id == model.FranchiseId)).Grade;
            foreach (var grade in grades)
            {
                var logsForGrade = climbLogs.Where(x => grade.Id.Equals(x.Climb.GradeId));

                if (logsForGrade.Count() == 0)
                {
                    continue;
                }

                var tops = logsForGrade.Count(x => x.Type == "Top");
                var attempt = logsForGrade.Count(x => x.Type == "Attempt");
                var uniqueClimbs = logsForGrade.DistinctBy(x => x.ClimbId).Count();
                var totalClimbs = logsForGrade.Count();

                // Sucecss rate
                model.GradeSuccessRate_Values.Add(Math.Round(1.0 * tops / totalClimbs * 100, 2));
                model.GradeSuccessRate_Label.Add(grade.ColorName);

                // Attempt:Top ratio
                model.GradeRatioAttempt_Values.Add(Math.Round(1.0 * attempt / totalClimbs, 2));
                model.GradeRatioTop_Values.Add(Math.Round(1.0 * tops / totalClimbs, 2));
                model.GradeRatioAttempt_Label.Add(grade.ColorName);

                // Average attempts
                model.GradeAverageAttempt_Values.Add(Math.Round(1.0 * totalClimbs / uniqueClimbs, 2));
                model.GradeAverageAttempt_Label.Add(grade.ColorName);

                // Untopped
                var climbsWithoutTops = logsForGrade.GroupBy(x => x.ClimbId, x => x.Type).Count(x => !x.Any(x => x == "Top"));
                model.Untopped_Values.Add(climbsWithoutTops);
                model.Untopped_Label.Add(grade.ColorName);
            }

            return View(model);
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
                return Content("Done");
            }

            return BadRequest("can't update climb log");
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
