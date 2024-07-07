using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Boulderlog.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBlue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Grade",
                keyColumn: "Id",
                keyValue: 14,
                column: "ColorHex",
                value: "#0033ff");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Grade",
                keyColumn: "Id",
                keyValue: 14,
                column: "ColorHex",
                value: "#003153");
        }
    }
}
