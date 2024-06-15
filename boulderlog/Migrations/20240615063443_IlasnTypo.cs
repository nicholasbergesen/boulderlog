using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Boulderlog.Migrations
{
    /// <inheritdoc />
    public partial class IlasnTypo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Gym",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Ilsan");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Gym",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Ilsam");
        }
    }
}
