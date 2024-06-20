using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Boulderlog.Migrations
{
    /// <inheritdoc />
    public partial class AddCheeseClimbing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SessionFilter_Franchise_FranchiseId",
                table: "SessionFilter");

            migrationBuilder.DropIndex(
                name: "IX_SessionFilter_FranchiseId",
                table: "SessionFilter");

            migrationBuilder.DropColumn(
                name: "FranchiseId",
                table: "SessionFilter");

            migrationBuilder.DropColumn(
                name: "HoldColor",
                table: "SessionFilter");

            migrationBuilder.InsertData(
                table: "Franchise",
                columns: new[] { "Id", "Name" },
                values: new object[] { 3, "Cheese Climbing" });

            migrationBuilder.UpdateData(
                table: "Grade",
                keyColumn: "Id",
                keyValue: 11,
                column: "ColorHex",
                value: "#ede932");

            migrationBuilder.UpdateData(
                table: "Grade",
                keyColumn: "Id",
                keyValue: 14,
                column: "ColorHex",
                value: "#003153");

            migrationBuilder.UpdateData(
                table: "Grade",
                keyColumn: "Id",
                keyValue: 16,
                column: "ColorHex",
                value: "#860d86");

            migrationBuilder.InsertData(
                table: "Grade",
                columns: new[] { "Id", "ColorHex", "ColorName", "FranchiseId", "SortOrder", "VScale" },
                values: new object[,]
                {
                    { 20, "#FF0000", "Red", 3, 1, "V0" },
                    { 21, "#FFA500", "Orange", 3, 2, "V1" },
                    { 22, "#ede932", "Yellow", 3, 3, "V2" },
                    { 23, "#008000", "Green", 3, 4, "V3" },
                    { 24, "#207be4", "Blue", 3, 5, "V4" },
                    { 25, "#003153", "Navy", 3, 6, "V5" },
                    { 26, "#860d86", "Purple", 3, 7, "V6" },
                    { 27, "#000000", "Black", 3, 8, "V7" }
                });

            migrationBuilder.InsertData(
                table: "Gym",
                columns: new[] { "Id", "FranchiseId", "Name", "Walls" },
                values: new object[] { 12, 3, "Yongsan", "Mozza;Cheddar" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Grade",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Grade",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Grade",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Grade",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Grade",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Grade",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Grade",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Grade",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Gym",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Franchise",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.AddColumn<int>(
                name: "FranchiseId",
                table: "SessionFilter",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HoldColor",
                table: "SessionFilter",
                type: "TEXT",
                maxLength: 255,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Grade",
                keyColumn: "Id",
                keyValue: 11,
                column: "ColorHex",
                value: "#FFFF00");

            migrationBuilder.UpdateData(
                table: "Grade",
                keyColumn: "Id",
                keyValue: 14,
                column: "ColorHex",
                value: "#0000FF");

            migrationBuilder.UpdateData(
                table: "Grade",
                keyColumn: "Id",
                keyValue: 16,
                column: "ColorHex",
                value: "#800080");

            migrationBuilder.CreateIndex(
                name: "IX_SessionFilter_FranchiseId",
                table: "SessionFilter",
                column: "FranchiseId");

            migrationBuilder.AddForeignKey(
                name: "FK_SessionFilter_Franchise_FranchiseId",
                table: "SessionFilter",
                column: "FranchiseId",
                principalTable: "Franchise",
                principalColumn: "Id");
        }
    }
}
