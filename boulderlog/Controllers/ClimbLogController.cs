using Boulderlog.Data;
using Boulderlog.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boulderlog.Controllers
{
    public class ClimbLogController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClimbLogController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ClimbLog
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ClimbLog.Include(c => c.Climb);
            return View(await applicationDbContext.ToListAsync());
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
            ViewData["Type"] = new SelectList(new List<string>() { "Attempt", "Top", "Flash" }, "Attempt");
            ViewBag.ClimbId = climbId;
            return View(new ClimbLog());
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
