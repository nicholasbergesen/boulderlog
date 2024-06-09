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
    [Migration("20240608040753_Create-Gym-Grade-Tables")]
    partial class CreateGymGradeTables
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

                    b.Property<int?>("GradeId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("GradeOld")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.Property<int?>("GymId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("GymOld")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.Property<string>("HoldColor")
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.Property<string>("ImageId")
                        .IsRequired()
                        .HasMaxLength(36)
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Wall")
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("GradeId");

                    b.HasIndex("GymId");

                    b.HasIndex("UserId");

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

                    b.HasKey("Id");

                    b.HasIndex("ClimbId");

                    b.ToTable("ClimbLog");
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

                    b.Property<int>("GymId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SortOrder")
                        .HasColumnType("INTEGER");

                    b.Property<string>("VScale")
                        .IsRequired()
                        .HasMaxLength(3)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("GymId");

                    b.ToTable("Grade");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ColorHex = "#FFFFFF",
                            ColorName = "1",
                            GymId = 1,
                            SortOrder = 1,
                            VScale = "V0"
                        },
                        new
                        {
                            Id = 2,
                            ColorHex = "#FFFF00",
                            ColorName = "2",
                            GymId = 1,
                            SortOrder = 2,
                            VScale = "V0"
                        },
                        new
                        {
                            Id = 3,
                            ColorHex = "#FFA500",
                            ColorName = "3",
                            GymId = 1,
                            SortOrder = 3,
                            VScale = "V1"
                        },
                        new
                        {
                            Id = 4,
                            ColorHex = "#008000",
                            ColorName = "4",
                            GymId = 1,
                            SortOrder = 4,
                            VScale = "V1"
                        },
                        new
                        {
                            Id = 5,
                            ColorHex = "#0000FF",
                            ColorName = "5",
                            GymId = 1,
                            SortOrder = 5,
                            VScale = "V2"
                        },
                        new
                        {
                            Id = 6,
                            ColorHex = "#FF0000",
                            ColorName = "6",
                            GymId = 1,
                            SortOrder = 6,
                            VScale = "V2"
                        },
                        new
                        {
                            Id = 7,
                            ColorHex = "#800080",
                            ColorName = "7",
                            GymId = 1,
                            SortOrder = 7,
                            VScale = "V3"
                        },
                        new
                        {
                            Id = 8,
                            ColorHex = "#808080",
                            ColorName = "8",
                            GymId = 1,
                            SortOrder = 8,
                            VScale = "V4"
                        },
                        new
                        {
                            Id = 9,
                            ColorHex = "#A52A2A",
                            ColorName = "9",
                            GymId = 1,
                            SortOrder = 9,
                            VScale = "V5"
                        },
                        new
                        {
                            Id = 10,
                            ColorHex = "#FFFFFF",
                            ColorName = "White",
                            GymId = 2,
                            SortOrder = 1,
                            VScale = "V0"
                        },
                        new
                        {
                            Id = 11,
                            ColorHex = "#FFFF00",
                            ColorName = "Yellow",
                            GymId = 2,
                            SortOrder = 2,
                            VScale = "V1"
                        },
                        new
                        {
                            Id = 12,
                            ColorHex = "#FFA500",
                            ColorName = "Orange",
                            GymId = 2,
                            SortOrder = 3,
                            VScale = "V2"
                        },
                        new
                        {
                            Id = 13,
                            ColorHex = "#008000",
                            ColorName = "Green",
                            GymId = 2,
                            SortOrder = 4,
                            VScale = "V3"
                        },
                        new
                        {
                            Id = 14,
                            ColorHex = "#0000FF",
                            ColorName = "Blue",
                            GymId = 2,
                            SortOrder = 5,
                            VScale = "V4"
                        },
                        new
                        {
                            Id = 15,
                            ColorHex = "#FF0000",
                            ColorName = "Red",
                            GymId = 2,
                            SortOrder = 6,
                            VScale = "V5"
                        },
                        new
                        {
                            Id = 16,
                            ColorHex = "#800080",
                            ColorName = "Purple",
                            GymId = 2,
                            SortOrder = 7,
                            VScale = "V6"
                        },
                        new
                        {
                            Id = 17,
                            ColorHex = "#808080",
                            ColorName = "Grey",
                            GymId = 2,
                            SortOrder = 8,
                            VScale = "V7"
                        },
                        new
                        {
                            Id = 18,
                            ColorHex = "#A52A2A",
                            ColorName = "Brown",
                            GymId = 2,
                            SortOrder = 9,
                            VScale = "V8"
                        },
                        new
                        {
                            Id = 19,
                            ColorHex = "#000000",
                            ColorName = "Black",
                            GymId = 2,
                            SortOrder = 10,
                            VScale = "V9"
                        });
                });

            modelBuilder.Entity("Boulderlog.Data.Models.Gym", b =>
                {
                    b.Property<int?>("Id")
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

                    b.ToTable("Gym");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "TheClimb-B-Hongdae",
                            Walls = "Sector1;Sector2"
                        },
                        new
                        {
                            Id = 2,
                            Name = "TheClimb-Yeonnam",
                            Walls = "Yeonnam;Toitmaru;Sinchon"
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

            modelBuilder.Entity("Boulderlog.Data.Models.ClimbLog", b =>
                {
                    b.HasOne("Boulderlog.Data.Models.Climb", "Climb")
                        .WithMany("ClimbLogs")
                        .HasForeignKey("ClimbId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Climb");
                });

            modelBuilder.Entity("Boulderlog.Data.Models.Grade", b =>
                {
                    b.HasOne("Boulderlog.Data.Models.Gym", "Gym")
                        .WithMany()
                        .HasForeignKey("GymId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Gym");
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
#pragma warning restore 612, 618
        }
    }
}
