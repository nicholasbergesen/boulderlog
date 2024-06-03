using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Boulderlog.Migrations
{
    /// <inheritdoc />
    public partial class CreateGymandGradeTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Climb",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "GradeId",
                table: "Climb",
                type: "TEXT",
                maxLength: 36,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "GymId",
                table: "Climb",
                type: "TEXT",
                maxLength: 36,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Gym",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", maxLength: 36, nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    Walls = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gym", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Grade",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", maxLength: 36, nullable: false),
                    ColorHex = table.Column<string>(type: "TEXT", maxLength: 7, nullable: false),
                    ColorName = table.Column<string>(type: "TEXT", maxLength: 25, nullable: false),
                    SortOrder = table.Column<int>(type: "INTEGER", nullable: false),
                    VScale = table.Column<string>(type: "TEXT", maxLength: 3, nullable: false),
                    GymId = table.Column<string>(type: "TEXT", maxLength: 36, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grade", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Grade_Gym_GymId",
                        column: x => x.GymId,
                        principalTable: "Gym",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Climb_GradeId",
                table: "Climb",
                column: "GradeId");

            migrationBuilder.CreateIndex(
                name: "IX_Climb_GymId",
                table: "Climb",
                column: "GymId");

            migrationBuilder.CreateIndex(
                name: "IX_Grade_GymId",
                table: "Grade",
                column: "GymId");

            migrationBuilder.AddForeignKey(
                name: "FK_Climb_Grade_GradeId",
                table: "Climb",
                column: "GradeId",
                principalTable: "Grade",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Climb_Grade_GymId",
                table: "Climb",
                column: "GymId",
                principalTable: "Grade",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Climb_Grade_GradeId",
                table: "Climb");

            migrationBuilder.DropForeignKey(
                name: "FK_Climb_Grade_GymId",
                table: "Climb");

            migrationBuilder.DropTable(
                name: "Grade");

            migrationBuilder.DropTable(
                name: "Gym");

            migrationBuilder.DropIndex(
                name: "IX_Climb_GradeId",
                table: "Climb");

            migrationBuilder.DropIndex(
                name: "IX_Climb_GymId",
                table: "Climb");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Climb");

            migrationBuilder.DropColumn(
                name: "GradeId",
                table: "Climb");

            migrationBuilder.DropColumn(
                name: "GymId",
                table: "Climb");
        }
    }
}
