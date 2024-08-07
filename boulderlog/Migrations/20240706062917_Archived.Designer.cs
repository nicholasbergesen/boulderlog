﻿// <auto-generated />
using System;
using Boulderlog.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Boulderlog.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240706062917_Archived")]
    partial class Archived
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.4");

            modelBuilder.Entity("Boulderlog.Data.Models.AppRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Boulderlog.Data.Models.AppUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("TEXT");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("TEXT");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Boulderlog.Data.Models.Climb", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(36)
                        .HasColumnType("TEXT");

                    b.Property<string>("CreatedByUserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("FranchiseId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("GradeId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("GymId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("HoldColor")
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.Property<string>("ImageId")
                        .IsRequired()
                        .HasMaxLength(36)
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsArchived")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("TimeStamp")
                        .HasColumnType("TEXT");

                    b.Property<string>("Wall")
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CreatedByUserId");

                    b.HasIndex("FranchiseId");

                    b.HasIndex("GradeId");

                    b.HasIndex("GymId");

                    b.ToTable("Climb");
                });

            modelBuilder.Entity("Boulderlog.Data.Models.ClimbLog", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(36)
                        .HasColumnType("TEXT");

                    b.Property<string>("ClimbId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("TimeStamp")
                        .HasColumnType("TEXT");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ClimbId");

                    b.HasIndex("UserId");

                    b.ToTable("ClimbLog");
                });

            modelBuilder.Entity("Boulderlog.Data.Models.Franchise", b =>
                {
                    b.Property<int?>("Id")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Franchise");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "TheClimb-B"
                        },
                        new
                        {
                            Id = 2,
                            Name = "TheClimb"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Cheese Climbing"
                        });
                });

            modelBuilder.Entity("Boulderlog.Data.Models.Grade", b =>
                {
                    b.Property<int?>("Id")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ColorHex")
                        .IsRequired()
                        .HasMaxLength(7)
                        .HasColumnType("TEXT");

                    b.Property<string>("ColorName")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("TEXT");

                    b.Property<int?>("FranchiseId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SortOrder")
                        .HasColumnType("INTEGER");

                    b.Property<string>("VScale")
                        .IsRequired()
                        .HasMaxLength(3)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("FranchiseId");

                    b.ToTable("Grade");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ColorHex = "#FFFFFF",
                            ColorName = "1",
                            FranchiseId = 1,
                            SortOrder = 1,
                            VScale = "V0"
                        },
                        new
                        {
                            Id = 2,
                            ColorHex = "#FFFF00",
                            ColorName = "2",
                            FranchiseId = 1,
                            SortOrder = 2,
                            VScale = "V0"
                        },
                        new
                        {
                            Id = 3,
                            ColorHex = "#FFA500",
                            ColorName = "3",
                            FranchiseId = 1,
                            SortOrder = 3,
                            VScale = "V1"
                        },
                        new
                        {
                            Id = 4,
                            ColorHex = "#008000",
                            ColorName = "4",
                            FranchiseId = 1,
                            SortOrder = 4,
                            VScale = "V1"
                        },
                        new
                        {
                            Id = 5,
                            ColorHex = "#0000FF",
                            ColorName = "5",
                            FranchiseId = 1,
                            SortOrder = 5,
                            VScale = "V2"
                        },
                        new
                        {
                            Id = 6,
                            ColorHex = "#FF0000",
                            ColorName = "6",
                            FranchiseId = 1,
                            SortOrder = 6,
                            VScale = "V2"
                        },
                        new
                        {
                            Id = 7,
                            ColorHex = "#800080",
                            ColorName = "7",
                            FranchiseId = 1,
                            SortOrder = 7,
                            VScale = "V3"
                        },
                        new
                        {
                            Id = 8,
                            ColorHex = "#808080",
                            ColorName = "8",
                            FranchiseId = 1,
                            SortOrder = 8,
                            VScale = "V4"
                        },
                        new
                        {
                            Id = 9,
                            ColorHex = "#A52A2A",
                            ColorName = "9",
                            FranchiseId = 1,
                            SortOrder = 9,
                            VScale = "V5"
                        },
                        new
                        {
                            Id = 10,
                            ColorHex = "#FFFFFF",
                            ColorName = "White",
                            FranchiseId = 2,
                            SortOrder = 1,
                            VScale = "V0"
                        },
                        new
                        {
                            Id = 11,
                            ColorHex = "#ede932",
                            ColorName = "Yellow",
                            FranchiseId = 2,
                            SortOrder = 2,
                            VScale = "V1"
                        },
                        new
                        {
                            Id = 12,
                            ColorHex = "#FFA500",
                            ColorName = "Orange",
                            FranchiseId = 2,
                            SortOrder = 3,
                            VScale = "V2"
                        },
                        new
                        {
                            Id = 13,
                            ColorHex = "#008000",
                            ColorName = "Green",
                            FranchiseId = 2,
                            SortOrder = 4,
                            VScale = "V3"
                        },
                        new
                        {
                            Id = 14,
                            ColorHex = "#003153",
                            ColorName = "Blue",
                            FranchiseId = 2,
                            SortOrder = 5,
                            VScale = "V4"
                        },
                        new
                        {
                            Id = 15,
                            ColorHex = "#FF0000",
                            ColorName = "Red",
                            FranchiseId = 2,
                            SortOrder = 6,
                            VScale = "V5"
                        },
                        new
                        {
                            Id = 16,
                            ColorHex = "#860d86",
                            ColorName = "Purple",
                            FranchiseId = 2,
                            SortOrder = 7,
                            VScale = "V6"
                        },
                        new
                        {
                            Id = 17,
                            ColorHex = "#808080",
                            ColorName = "Grey",
                            FranchiseId = 2,
                            SortOrder = 8,
                            VScale = "V7"
                        },
                        new
                        {
                            Id = 18,
                            ColorHex = "#AB5236",
                            ColorName = "Brown",
                            FranchiseId = 2,
                            SortOrder = 9,
                            VScale = "V8"
                        },
                        new
                        {
                            Id = 19,
                            ColorHex = "#000000",
                            ColorName = "Black",
                            FranchiseId = 2,
                            SortOrder = 10,
                            VScale = "V9"
                        },
                        new
                        {
                            Id = 20,
                            ColorHex = "#FF0000",
                            ColorName = "Red",
                            FranchiseId = 3,
                            SortOrder = 1,
                            VScale = "V0"
                        },
                        new
                        {
                            Id = 21,
                            ColorHex = "#FFA500",
                            ColorName = "Orange",
                            FranchiseId = 3,
                            SortOrder = 2,
                            VScale = "V1"
                        },
                        new
                        {
                            Id = 22,
                            ColorHex = "#ede932",
                            ColorName = "Yellow",
                            FranchiseId = 3,
                            SortOrder = 3,
                            VScale = "V2"
                        },
                        new
                        {
                            Id = 23,
                            ColorHex = "#008000",
                            ColorName = "Green",
                            FranchiseId = 3,
                            SortOrder = 4,
                            VScale = "V3"
                        },
                        new
                        {
                            Id = 24,
                            ColorHex = "#207be4",
                            ColorName = "Blue",
                            FranchiseId = 3,
                            SortOrder = 5,
                            VScale = "V4"
                        },
                        new
                        {
                            Id = 25,
                            ColorHex = "#003153",
                            ColorName = "Navy",
                            FranchiseId = 3,
                            SortOrder = 6,
                            VScale = "V5"
                        },
                        new
                        {
                            Id = 26,
                            ColorHex = "#860d86",
                            ColorName = "Purple",
                            FranchiseId = 3,
                            SortOrder = 7,
                            VScale = "V6"
                        },
                        new
                        {
                            Id = 27,
                            ColorHex = "#000000",
                            ColorName = "Black",
                            FranchiseId = 3,
                            SortOrder = 8,
                            VScale = "V7"
                        });
                });

            modelBuilder.Entity("Boulderlog.Data.Models.Gym", b =>
                {
                    b.Property<int?>("Id")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("FranchiseId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.Property<string>("Walls")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("FranchiseId");

                    b.ToTable("Gym");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            FranchiseId = 1,
                            Name = "Hongdae",
                            Walls = "Sector1;Sector2"
                        },
                        new
                        {
                            Id = 2,
                            FranchiseId = 2,
                            Name = "Yeonnam",
                            Walls = "Yeonnam;Toitmaru;Sinchon"
                        },
                        new
                        {
                            Id = 3,
                            FranchiseId = 2,
                            Name = "Ilsan",
                            Walls = "New Wave;Comp;White;Island A;Island B"
                        },
                        new
                        {
                            Id = 4,
                            FranchiseId = 2,
                            Name = "Magok",
                            Walls = "Sector 1-2;Sector 3-4;Sector 5-6;Sector 7-8"
                        },
                        new
                        {
                            Id = 5,
                            FranchiseId = 2,
                            Name = "SNU",
                            Walls = "Vertical;Margalef;Arhi;Cone;Hexagon"
                        },
                        new
                        {
                            Id = 6,
                            FranchiseId = 2,
                            Name = "Sinsa",
                            Walls = "Serosu;Darosu;Narosu;Garosu"
                        },
                        new
                        {
                            Id = 7,
                            FranchiseId = 2,
                            Name = "Sillim",
                            Walls = "Galaxy Balance;Galaxy Overhang;Milky Way;Andromeda"
                        },
                        new
                        {
                            Id = 8,
                            FranchiseId = 2,
                            Name = "Gangnam",
                            Walls = "Sector 1-2;Sector 3-4;Sector 5-6;Sector 7-8"
                        },
                        new
                        {
                            Id = 9,
                            FranchiseId = 2,
                            Name = "Sadang",
                            Walls = "Gwanak;Dongjak;Seocho"
                        },
                        new
                        {
                            Id = 10,
                            FranchiseId = 2,
                            Name = "Yangjae",
                            Walls = "Dungeon;Slab;Cave;Island;Flat;Arch;Prow"
                        },
                        new
                        {
                            Id = 11,
                            FranchiseId = 2,
                            Name = "Nonhyeon",
                            Walls = "Bat;Mini Bat;Gogae;Non"
                        },
                        new
                        {
                            Id = 12,
                            FranchiseId = 3,
                            Name = "Yongsan",
                            Walls = "Mozza;Cheddar"
                        });
                });

            modelBuilder.Entity("Boulderlog.Data.Models.Image", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(36)
                        .HasColumnType("TEXT");

                    b.Property<byte[]>("Bytes")
                        .IsRequired()
                        .HasColumnType("BLOB");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("FileType")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Image");
                });

            modelBuilder.Entity("Boulderlog.Data.Models.SessionFilter", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("GradeId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("GymId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("LastUpdated")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasMaxLength(36)
                        .HasColumnType("TEXT");

                    b.Property<string>("Wall")
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("GradeId");

                    b.HasIndex("GymId");

                    b.HasIndex("UserId");

                    b.ToTable("SessionFilter");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClaimType")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("TEXT");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClaimType")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("TEXT");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("TEXT");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("RoleId")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("TEXT");

                    b.Property<string>("Value")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Boulderlog.Data.Models.Climb", b =>
                {
                    b.HasOne("Boulderlog.Data.Models.AppUser", "CreatedByUser")
                        .WithMany()
                        .HasForeignKey("CreatedByUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Boulderlog.Data.Models.Franchise", "Franchise")
                        .WithMany()
                        .HasForeignKey("FranchiseId");

                    b.HasOne("Boulderlog.Data.Models.Grade", "Grade")
                        .WithMany()
                        .HasForeignKey("GradeId");

                    b.HasOne("Boulderlog.Data.Models.Gym", "Gym")
                        .WithMany()
                        .HasForeignKey("GymId");

                    b.Navigation("CreatedByUser");

                    b.Navigation("Franchise");

                    b.Navigation("Grade");

                    b.Navigation("Gym");
                });

            modelBuilder.Entity("Boulderlog.Data.Models.ClimbLog", b =>
                {
                    b.HasOne("Boulderlog.Data.Models.Climb", "Climb")
                        .WithMany("ClimbLogs")
                        .HasForeignKey("ClimbId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Boulderlog.Data.Models.AppUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("Climb");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Boulderlog.Data.Models.Grade", b =>
                {
                    b.HasOne("Boulderlog.Data.Models.Franchise", "Franchise")
                        .WithMany("Grade")
                        .HasForeignKey("FranchiseId");

                    b.Navigation("Franchise");
                });

            modelBuilder.Entity("Boulderlog.Data.Models.Gym", b =>
                {
                    b.HasOne("Boulderlog.Data.Models.Franchise", "Franchise")
                        .WithMany("Gym")
                        .HasForeignKey("FranchiseId");

                    b.Navigation("Franchise");
                });

            modelBuilder.Entity("Boulderlog.Data.Models.SessionFilter", b =>
                {
                    b.HasOne("Boulderlog.Data.Models.Grade", "Grade")
                        .WithMany()
                        .HasForeignKey("GradeId");

                    b.HasOne("Boulderlog.Data.Models.Gym", "Gym")
                        .WithMany()
                        .HasForeignKey("GymId");

                    b.HasOne("Boulderlog.Data.Models.AppUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Grade");

                    b.Navigation("Gym");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Boulderlog.Data.Models.AppRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Boulderlog.Data.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Boulderlog.Data.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Boulderlog.Data.Models.AppRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Boulderlog.Data.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Boulderlog.Data.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Boulderlog.Data.Models.Climb", b =>
                {
                    b.Navigation("ClimbLogs");
                });

            modelBuilder.Entity("Boulderlog.Data.Models.Franchise", b =>
                {
                    b.Navigation("Grade");

                    b.Navigation("Gym");
                });
#pragma warning restore 612, 618
        }
    }
}
