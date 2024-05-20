using Boulderlog.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Boulderlog.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser, AppRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Climb> Climb { get; set; }
        public DbSet<ClimbLog> ClimbLog { get; set; }
        public DbSet<Image> Image { get; set; }
    }
}
