using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Boulderlog.Migrations
{
    /// <inheritdoc />
    public partial class RemoveOldGymGradeColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GradeOld",
                table: "Climb");

            migrationBuilder.DropColumn(
                name: "GymOld",
                table: "Climb");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GradeOld",
                table: "Climb",
                type: "TEXT",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "GymOld",
                table: "Climb",
                type: "TEXT",
                maxLength: 255,
                nullable: false,
                defaultValue: "");
        }
    }
}
