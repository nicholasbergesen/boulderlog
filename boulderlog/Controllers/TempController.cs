using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Boulderlog.Data;
using Boulderlog.Data.Models;

namespace Boulderlog.Controllers
{
    public class TempController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TempController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Temp
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Climb.Include(c => c.Grade).Include(c => c.Gym).Include(c => c.CreatedByUser);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Temp/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var climb = await _context.Climb
                .Include(c => c.Grade)
                .Include(c => c.Gym)
                .Include(c => c.CreatedByUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (climb == null)
            {
                return NotFound();
            }

            return View(climb);
        }

        // GET: Temp/Create
        public IActionResult Create()
        {
            ViewData["GradeId"] = new SelectList(_context.Grade, "Id", "Id");
            ViewData["GymId"] = new SelectList(_context.Gym, "Id", "Id");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Temp/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ImageId,GradeOld,HoldColor,GymOld,Wall,UserId,GymId,GradeId")] Climb climb)
        {
            if (ModelState.IsValid)
            {
                _context.Add(climb);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GradeId"] = new SelectList(_context.Grade, "Id", "Id", climb.GradeId);
            ViewData["GymId"] = new SelectList(_context.Gym, "Id", "Id", climb.GymId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", climb.CreatedByUserId);
            return View(climb);
        }

        // GET: Temp/Edit/5
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
            ViewData["GradeId"] = new SelectList(_context.Grade, "Id", "Id", climb.GradeId);
            ViewData["GymId"] = new SelectList(_context.Gym, "Id", "Id", climb.GymId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", climb.CreatedByUserId);
            return View(climb);
        }

        // POST: Temp/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,ImageId,GradeOld,HoldColor,GymOld,Wall,UserId,GymId,GradeId")] Climb climb)
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
            ViewData["GradeId"] = new SelectList(_context.Grade, "Id", "Id", climb.GradeId);
            ViewData["GymId"] = new SelectList(_context.Gym, "Id", "Id", climb.GymId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", climb.CreatedByUserId);
            return View(climb);
        }

        // GET: Temp/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var climb = await _context.Climb
                .Include(c => c.Grade)
                .Include(c => c.Gym)
                .Include(c => c.CreatedByUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (climb == null)
            {
                return NotFound();
            }

            return View(climb);
        }

        // POST: Temp/Delete/5
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
