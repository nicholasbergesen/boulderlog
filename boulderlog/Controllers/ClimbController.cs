using Boulderlog.Data;
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
using System.Diagnostics;
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

            var timeZone = TimeZoneInfo.FindSystemTimeZoneById("Korea Standard Time");
            var koreaTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZone);
            koreaTime = koreaTime.AddDays(-30);

            var climbs = _context.Climb
                .Where(x => !gymId.HasValue || gymId.Value.Equals(x.GymId))
                .Where(x => gradeId <= 0 || gradeId.Equals(x.GradeId))
                .Where(x => string.IsNullOrEmpty(wall) || wall.Equals(x.Wall))
                .Include(x => x.ClimbLogs)
                .Where(x => x.ClimbLogs.Where(x => !gymId.HasValue || x.TimeStamp > koreaTime).Select(x => x.UserId).Contains(userId))
                .Include(x => x.Gym)
                .Include(x => x.Grade)
                .OrderByDescending(x => x.ClimbLogs.Max(x => x.TimeStamp));

            List <ClimbViewModel> climbViewModels = new List<ClimbViewModel>();

            foreach (var climb in climbs)
            {
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
                    IsFlashed = "Top" == climb.ClimbLogs.OrderBy(x => x.TimeStamp).FirstOrDefault()?.Type
                };

                var attempts = climb.ClimbLogs
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

        public async Task<IActionResult> Session()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var sessionFilter = await _context.SessionFilter
                .Include(x => x.Gym)
                .FirstOrDefaultAsync(x => x.UserId == userId);

            if (sessionFilter == null)
            {
                var gyms = _context.Gym.Include(x => x.Franchise).Select(x => new { x.Id, x.Name, Group = new SelectListGroup { Name = x.Franchise.Name } });
                ViewData["Gym"] = new SelectList(gyms, "Id", "Name", null, "Group.Name");

                return View(new SessionPageViewModel()
                {
                    SessionFilter = new SessionFilter() { UserId = userId },
                    ClimbViewModels = new Dictionary<string, List<ClimbViewModel>>(),
                });
            }

            var climbs = _context.Climb
                .Include(x => x.Grade)
                .Include(x => x.ClimbLogs)
                .Where(x => !sessionFilter.GradeId.HasValue || sessionFilter.GradeId.Value.Equals(x.GradeId))
                .Where(x => string.IsNullOrEmpty(sessionFilter.Wall) || sessionFilter.Wall.Equals(x.Wall))
                .Distinct()
                .OrderByDescending(x => x.ClimbLogs.Max(x => x.TimeStamp));

            var climbViewModels = new Dictionary<string, List<ClimbViewModel>>();

            foreach (var climb in climbs)
            {
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
                    IsFlashed = "Top" == climb.ClimbLogs.OrderBy(x => x.TimeStamp).FirstOrDefault()?.Type
                };

                var attempts = climb.ClimbLogs
                    .Where(x => x.UserId == userId)
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

                if (climbViewModels.TryGetValue(climb.Wall, out List<ClimbViewModel> climbModels))
                {
                    climbModels.Add(climbModel);
                }
                else
                {
                    climbViewModels.Add(climb.Wall, new List<ClimbViewModel> { climbModel });
                }
            }

            var pageModel = new SessionPageViewModel();
            pageModel.SessionFilter = sessionFilter;
            pageModel.ClimbViewModels = climbViewModels;

            var gym = await _context.Gym
                .Include(x => x.Franchise.Grade)
                .FirstOrDefaultAsync(x => x.Id == sessionFilter.GymId);

            var grades = gym.Franchise.Grade.Select(x => new { x.Id, x.ColorName }).ToList();
            var walls = gym.Walls.Split(";").Select(x => new { Id = x, Val = x }).ToList();
            grades.Insert(0, new { Id = (int?)null, ColorName = "" });
            walls.Insert(0, new { Id = "", Val = "" });
            ViewData["Grade"] = new SelectList(grades, "Id", "ColorName", sessionFilter.GradeId);
            ViewData["Wall"] = new SelectList(walls, "Id", "Val", sessionFilter.Wall);

            return View(pageModel);
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
