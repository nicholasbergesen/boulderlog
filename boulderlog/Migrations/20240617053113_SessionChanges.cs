using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace Boulderlog.Migrations
{
    /// <inheritdoc />
    public partial class SessionChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SessionFilter",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<string>(type: "TEXT", maxLength: 36, nullable: false),
                    GymId = table.Column<int>(type: "INTEGER", nullable: true),
                    FranchiseId = table.Column<int>(type: "INTEGER", nullable: true),
                    GradeId = table.Column<int>(type: "INTEGER", nullable: true),
                    HoldColor = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    Wall = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    LastUpdated = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SessionFilter", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SessionFilter_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SessionFilter_Franchise_FranchiseId",
                        column: x => x.FranchiseId,
                        principalTable: "Franchise",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SessionFilter_Grade_GradeId",
                        column: x => x.GradeId,
                        principalTable: "Grade",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SessionFilter_Gym_GymId",
                        column: x => x.GymId,
                        principalTable: "Gym",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_SessionFilter_FranchiseId",
                table: "SessionFilter",
                column: "FranchiseId");

            migrationBuilder.CreateIndex(
                name: "IX_SessionFilter_GradeId",
                table: "SessionFilter",
                column: "GradeId");

            migrationBuilder.CreateIndex(
                name: "IX_SessionFilter_GymId",
                table: "SessionFilter",
                column: "GymId");

            migrationBuilder.CreateIndex(
                name: "IX_SessionFilter_UserId",
                table: "SessionFilter",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SessionFilter");
        }
    }
}
