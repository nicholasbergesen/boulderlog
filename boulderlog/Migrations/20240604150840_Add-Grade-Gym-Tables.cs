using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Boulderlog.Migrations
{
    /// <inheritdoc />
    public partial class AddGradeGymTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Gym",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
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
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    ColorHex = table.Column<string>(type: "TEXT", maxLength: 7, nullable: false),
                    ColorName = table.Column<string>(type: "TEXT", maxLength: 25, nullable: false),
                    SortOrder = table.Column<int>(type: "INTEGER", nullable: false),
                    VScale = table.Column<string>(type: "TEXT", maxLength: 3, nullable: false),
                    GymId = table.Column<int>(type: "INTEGER", maxLength: 36, nullable: false)
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

            migrationBuilder.InsertData(
                table: "Gym",
                columns: new[] { "Id", "Name", "Walls" },
                values: new object[,]
                {
                    { 1, "TheClimb-B-Hongdae", "Sector1;Sector2" },
                    { 2, "TheClimb-Yeonnam", "Yeonnam;Toitmaru;Sinchon" }
                });

            migrationBuilder.InsertData(
                table: "Grade",
                columns: new[] { "Id", "ColorHex", "ColorName", "GymId", "SortOrder", "VScale" },
                values: new object[,]
                {
                    { 1, "#FFFFFF", "1", 1, 1, "V0" },
                    { 2, "#FFFF00", "2", 1, 2, "V0" },
                    { 3, "#FFA500", "3", 1, 3, "V1" },
                    { 4, "#008000", "4", 1, 4, "V1" },
                    { 5, "#0000FF", "5", 1, 5, "V2" },
                    { 6, "#FF0000", "6", 1, 6, "V2" },
                    { 7, "#800080", "7", 1, 7, "V3" },
                    { 8, "#808080", "8", 1, 8, "V4" },
                    { 9, "#A52A2A", "9", 1, 9, "V5" },
                    { 10, "#FFFFFF", "White", 2, 1, "V0" },
                    { 11, "#FFFF00", "Yellow", 2, 2, "V1" },
                    { 12, "#FFA500", "Orange", 2, 3, "V2" },
                    { 13, "#008000", "Green", 2, 4, "V3" },
                    { 14, "#0000FF", "Blue", 2, 5, "V4" },
                    { 15, "#FF0000", "Red", 2, 6, "V5" },
                    { 16, "#800080", "Purple", 2, 7, "V6" },
                    { 17, "#808080", "Grey", 2, 8, "V7" },
                    { 18, "#A52A2A", "Brown", 2, 9, "V8" },
                    { 19, "#000000", "Black", 2, 10, "V9" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Grade_GymId",
                table: "Grade",
                column: "GymId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Grade");

            migrationBuilder.DropTable(
                name: "Gym");
        }
    }
}
