using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Boulderlog.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBrownColor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Grade",
                keyColumn: "Id",
                keyValue: 18,
                column: "ColorHex",
                value: "#AB5236");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Grade",
                keyColumn: "Id",
                keyValue: 18,
                column: "ColorHex",
                value: "#A52A2A");
        }
    }
}
