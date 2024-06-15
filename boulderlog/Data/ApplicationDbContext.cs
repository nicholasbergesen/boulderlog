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
            builder.Entity<Franchise>().HasData(new Franchise() { Id = 1, Name = "TheClimb-B" });
            builder.Entity<Gym>().HasData(new Gym { Id = 1, Name = "Hongdae", Walls = "Sector1;Sector2", FranchiseId = 1 });
            builder.Entity<Grade>().HasData(new Grade { Id = 1, FranchiseId = 1, VScale = "V0", SortOrder = 1, ColorName = "1", ColorHex = "#FFFFFF" });
            builder.Entity<Grade>().HasData(new Grade { Id = 2, FranchiseId = 1, VScale = "V0", SortOrder = 2, ColorName = "2", ColorHex = "#FFFF00" });
            builder.Entity<Grade>().HasData(new Grade { Id = 3, FranchiseId = 1, VScale = "V1", SortOrder = 3, ColorName = "3", ColorHex = "#FFA500" });
            builder.Entity<Grade>().HasData(new Grade { Id = 4, FranchiseId = 1, VScale = "V1", SortOrder = 4, ColorName = "4", ColorHex = "#008000" });
            builder.Entity<Grade>().HasData(new Grade { Id = 5, FranchiseId = 1, VScale = "V2", SortOrder = 5, ColorName = "5", ColorHex = "#0000FF" });
            builder.Entity<Grade>().HasData(new Grade { Id = 6, FranchiseId = 1, VScale = "V2", SortOrder = 6, ColorName = "6", ColorHex = "#FF0000" });
            builder.Entity<Grade>().HasData(new Grade { Id = 7, FranchiseId = 1, VScale = "V3", SortOrder = 7, ColorName = "7", ColorHex = "#800080" });
            builder.Entity<Grade>().HasData(new Grade { Id = 8, FranchiseId = 1, VScale = "V4", SortOrder = 8, ColorName = "8", ColorHex = "#808080" });
            builder.Entity<Grade>().HasData(new Grade { Id = 9, FranchiseId = 1, VScale = "V5", SortOrder = 9, ColorName = "9", ColorHex = "#A52A2A" });


            builder.Entity<Franchise>().HasData(new Franchise() { Id = 2, Name = "TheClimb" });
            builder.Entity<Gym>().HasData(new Gym { Id = 2, Name = "Yeonnam", Walls = "Yeonnam;Toitmaru;Sinchon", FranchiseId = 2 });
            builder.Entity<Gym>().HasData(new Gym { Id = 3, Name = "Ilsan", Walls = "New Wave;Comp;White;Island A;Island B", FranchiseId = 2 });
            builder.Entity<Gym>().HasData(new Gym { Id = 4, Name = "Magok", Walls = "Sector 1-2;Sector 3-4;Sector 5-6;Sector 7-8", FranchiseId = 2 });
            builder.Entity<Gym>().HasData(new Gym { Id = 5, Name = "SNU", Walls = "Vertical;Margalef;Arhi;Cone;Hexagon", FranchiseId = 2 });
            builder.Entity<Gym>().HasData(new Gym { Id = 6, Name = "Sinsa", Walls = "Serosu;Darosu;Narosu;Garosu", FranchiseId = 2 });
            builder.Entity<Gym>().HasData(new Gym { Id = 7, Name = "Sillim", Walls = "Galaxy Balance;Galaxy Overhang;Milky Way;Andromeda", FranchiseId = 2 });
            builder.Entity<Gym>().HasData(new Gym { Id = 8, Name = "Gangnam", Walls = "Sector 1-2;Sector 3-4;Sector 5-6;Sector 7-8", FranchiseId = 2 });
            builder.Entity<Gym>().HasData(new Gym { Id = 9, Name = "Sadang", Walls = "Gwanak;Dongjak;Seocho", FranchiseId = 2 });
            builder.Entity<Gym>().HasData(new Gym { Id = 10, Name = "Yangjae", Walls = "Dungeon;Slab;Cave;Island;Flat;Arch;Prow", FranchiseId = 2 });
            builder.Entity<Gym>().HasData(new Gym { Id = 11, Name = "Nonhyeon", Walls = "Bat;Mini Bat;Gogae;Non", FranchiseId = 2 });
            builder.Entity<Grade>().HasData(new Grade { Id = 10, FranchiseId = 2, VScale = "V0", SortOrder = 1, ColorName = "White", ColorHex = "#FFFFFF" });
            builder.Entity<Grade>().HasData(new Grade { Id = 11, FranchiseId = 2, VScale = "V1", SortOrder = 2, ColorName = "Yellow", ColorHex = "#FFFF00" });
            builder.Entity<Grade>().HasData(new Grade { Id = 12, FranchiseId = 2, VScale = "V2", SortOrder = 3, ColorName = "Orange", ColorHex = "#FFA500" });
            builder.Entity<Grade>().HasData(new Grade { Id = 13, FranchiseId = 2, VScale = "V3", SortOrder = 4, ColorName = "Green", ColorHex = "#008000" });
            builder.Entity<Grade>().HasData(new Grade { Id = 14, FranchiseId = 2, VScale = "V4", SortOrder = 5, ColorName = "Blue", ColorHex = "#0000FF" });
            builder.Entity<Grade>().HasData(new Grade { Id = 15, FranchiseId = 2, VScale = "V5", SortOrder = 6, ColorName = "Red", ColorHex = "#FF0000" });
            builder.Entity<Grade>().HasData(new Grade { Id = 16, FranchiseId = 2, VScale = "V6", SortOrder = 7, ColorName = "Purple", ColorHex = "#800080" });
            builder.Entity<Grade>().HasData(new Grade { Id = 17, FranchiseId = 2, VScale = "V7", SortOrder = 8, ColorName = "Grey", ColorHex = "#808080" });
            builder.Entity<Grade>().HasData(new Grade { Id = 18, FranchiseId = 2, VScale = "V8", SortOrder = 9, ColorName = "Brown", ColorHex = "#A52A2A" });
            builder.Entity<Grade>().HasData(new Grade { Id = 19, FranchiseId = 2, VScale = "V9", SortOrder = 10, ColorName = "Black", ColorHex = "#000000" });

            base.OnModelCreating(builder);
        }

        public DbSet<Climb> Climb { get; set; }
        public DbSet<ClimbLog> ClimbLog { get; set; }
        public DbSet<Image> Image { get; set; }
        public DbSet<Gym> Gym { get; set; }
        public DbSet<Grade> Grade { get; set; }
        public DbSet<Franchise> Franchise { get; set; }
    }
}
