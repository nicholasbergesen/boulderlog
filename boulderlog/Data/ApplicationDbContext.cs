using Boulderlog.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;

namespace Boulderlog.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser, AppRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Gym>().HasData(new Gym { Id = 1, Name = "TheClimb-B-Hongdae", Walls = "Sector1;Sector2" });
            builder.Entity<Grade>().HasData(new Grade { Id = 1, GymId = 1, VScale = "V0", SortOrder = 1, ColorName = "1", ColorHex = "#FFFFFF" });
            builder.Entity<Grade>().HasData(new Grade { Id = 2, GymId = 1, VScale = "V0", SortOrder = 2, ColorName = "2", ColorHex = "#FFFF00" });
            builder.Entity<Grade>().HasData(new Grade { Id = 3, GymId = 1, VScale = "V1", SortOrder = 3, ColorName = "3", ColorHex = "#FFA500" });
            builder.Entity<Grade>().HasData(new Grade { Id = 4, GymId = 1, VScale = "V1", SortOrder = 4, ColorName = "4", ColorHex = "#008000" });
            builder.Entity<Grade>().HasData(new Grade { Id = 5, GymId = 1, VScale = "V2", SortOrder = 5, ColorName = "5", ColorHex = "#0000FF" });
            builder.Entity<Grade>().HasData(new Grade { Id = 6, GymId = 1, VScale = "V2", SortOrder = 6, ColorName = "6", ColorHex = "#FF0000" });
            builder.Entity<Grade>().HasData(new Grade { Id = 7, GymId = 1, VScale = "V3", SortOrder = 7, ColorName = "7", ColorHex = "#800080" });
            builder.Entity<Grade>().HasData(new Grade { Id = 8, GymId = 1, VScale = "V4", SortOrder = 8, ColorName = "8", ColorHex = "#808080" });
            builder.Entity<Grade>().HasData(new Grade { Id = 9, GymId = 1, VScale = "V5", SortOrder = 9, ColorName = "9", ColorHex = "#A52A2A" });

            builder.Entity<Gym>().HasData(new Gym { Id = 2, Name = "TheClimb-Yeonnam", Walls = "Yeonnam;Toitmaru;Sinchon" });
            builder.Entity<Grade>().HasData(new Grade { Id = 10, GymId = 2, VScale = "V0", SortOrder = 1, ColorName = "White", ColorHex = "#FFFFFF" });
            builder.Entity<Grade>().HasData(new Grade { Id = 11, GymId = 2, VScale = "V1", SortOrder = 2, ColorName = "Yellow", ColorHex = "#FFFF00" });
            builder.Entity<Grade>().HasData(new Grade { Id = 12, GymId = 2, VScale = "V2", SortOrder = 3, ColorName = "Orange", ColorHex = "#FFA500" });
            builder.Entity<Grade>().HasData(new Grade { Id = 13, GymId = 2, VScale = "V3", SortOrder = 4, ColorName = "Green", ColorHex = "#008000" });
            builder.Entity<Grade>().HasData(new Grade { Id = 14, GymId = 2, VScale = "V4", SortOrder = 5, ColorName = "Blue", ColorHex = "#0000FF" });
            builder.Entity<Grade>().HasData(new Grade { Id = 15, GymId = 2, VScale = "V5", SortOrder = 6, ColorName = "Red", ColorHex = "#FF0000" });
            builder.Entity<Grade>().HasData(new Grade { Id = 16, GymId = 2, VScale = "V6", SortOrder = 7, ColorName = "Purple", ColorHex = "#800080" });
            builder.Entity<Grade>().HasData(new Grade { Id = 17, GymId = 2, VScale = "V7", SortOrder = 8, ColorName = "Grey", ColorHex = "#808080" });
            builder.Entity<Grade>().HasData(new Grade { Id = 18, GymId = 2, VScale = "V8", SortOrder = 9, ColorName = "Brown", ColorHex = "#A52A2A" });
            builder.Entity<Grade>().HasData(new Grade { Id = 19, GymId = 2, VScale = "V9", SortOrder = 10, ColorName = "Black", ColorHex = "#000000" });

            base.OnModelCreating(builder);
        }

        public DbSet<Climb> Climb { get; set; }
        public DbSet<ClimbLog> ClimbLog { get; set; }
        public DbSet<Image> Image { get; set; }
        public DbSet<Gym> Gym { get; set; }
        public DbSet<Grade> Grade { get; set; }
    }
}
