using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Boulderlog.Migrations
{
    /// <inheritdoc />
    public partial class ClimbGrade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Color",
                table: "Climb",
                newName: "Grade");

            migrationBuilder.AddColumn<string>(
                name: "HoldColor",
                table: "Climb",
                type: "TEXT",
                maxLength: 255,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HoldColor",
                table: "Climb");

            migrationBuilder.RenameColumn(
                name: "Grade",
                table: "Climb",
                newName: "Color");
        }
    }
}
