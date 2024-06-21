using Boulderlog.Data;
using Boulderlog.Domain;
using Boulderlog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Boulderlog.ApiController
{
    [Route("api/[controller]")]
    [ApiController]
    public class GymController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public GymController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Gyms/5
        [HttpGet("{id}")]
        [ResponseCache(Location = ResponseCacheLocation.Any, Duration = 31536000, VaryByQueryKeys = ["id"])]
        public async Task<ActionResult<FilterViewModel>> GetGym(int? id)
        {
            var selectedGym = await _context.Gym
                .Include(x => x.Franchise.Grade)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (selectedGym == null)
            {
                return NotFound();
            }

            var model = new FilterViewModel();
            var grades = selectedGym.Franchise.Grade.Select(x =>
            new ColorDTO()
            {
                Id = x.Id.Value,
                ColorHex = x.ColorHex,
                ColorName = x.ColorName,
            }).ToList();
            //grades.Insert(0, new { Id = (int?)-1, ColorName = "" });
            //model.Grade = new SelectList(grades, "Id", "ColorName");

            var walls = selectedGym.Walls.Split(";").ToList();
            walls.Insert(0, "");
            model.Wall = new SelectList(walls);
            model.Grade = grades;

            return model;
        }
    }
}
