using Boulderlog.Data;
using Boulderlog.Data.Models;
using Boulderlog.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Threading.Tasks;
using static Boulderlog.Domain.Const;

namespace Boulderlog.Controllers
{
    [Route("[controller]")]
    public class MaintenanceController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly AppConfigOptions _appConfigOptions;

        public MaintenanceController(ApplicationDbContext context, UserManager<AppUser> userManager, IOptions<AppConfigOptions> appConfigOptions)
        {
            _context = context;
            _userManager = userManager;
            _appConfigOptions = appConfigOptions.Value;
        }
    }
}
