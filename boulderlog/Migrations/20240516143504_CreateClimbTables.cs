using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Boulderlog.Migrations
{
    /// <inheritdoc />
    public partial class CreateClimbTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Climb",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Color = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    Gym = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    Wall = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Climb", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ClimbLog",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    TimeStamp = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Type = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    ClimbId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClimbLog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClimbLog_Climb_ClimbId",
                        column: x => x.ClimbId,
                        principalTable: "Climb",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClimbLog_ClimbId",
                table: "ClimbLog",
                column: "ClimbId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClimbLog");

            migrationBuilder.DropTable(
                name: "Climb");
        }
    }
}
