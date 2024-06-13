using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Boulderlog.Migrations
{
    /// <inheritdoc />
    public partial class PopulateGyms : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Gym",
                columns: new[] { "Id", "FranchiseId", "Name", "Walls" },
                values: new object[,]
                {
                    { 3, 2, "Ilsam", "New Wave;Comp;White;Island A;Island B" },
                    { 4, 2, "Magok", "Sector 1-2;Sector 3-4;Sector 5-6;Sector 7-8" },
                    { 5, 2, "SNU", "Vertical;Margalef;Arhi;Cone;Hexagon" },
                    { 6, 2, "Sinsa", "Serosu;Darosu;Narosu;Garosu" },
                    { 7, 2, "Sillim", "Galaxy Balance;Galaxy Overhang;Milky Way;Andromeda" },
                    { 8, 2, "Gangnam", "Sector 1-2;Sector 3-4;Sector 5-6;Sector 7-8" },
                    { 9, 2, "Sadang", "Gwanak;Dongjak;Seocho" },
                    { 10, 2, "Yangjae", "Dungeon;Slab;Cave;Island;Flat;Arch;Prow" },
                    { 11, 2, "Nonhyeon", "Bat;Mini Bat;Gogae;Non" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Gym",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Gym",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Gym",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Gym",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Gym",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Gym",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Gym",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Gym",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Gym",
                keyColumn: "Id",
                keyValue: 11);
        }
    }
}
