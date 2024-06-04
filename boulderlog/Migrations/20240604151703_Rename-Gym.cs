using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Boulderlog.Migrations
{
    /// <inheritdoc />
    public partial class RenameGym : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Gym",
                table: "Climb",
                newName: "GymOld");

            migrationBuilder.RenameColumn(
                name: "Grade",
                table: "Climb",
                newName: "GradeOld");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "GymOld",
                table: "Climb",
                newName: "Gym");

            migrationBuilder.RenameColumn(
                name: "GradeOld",
                table: "Climb",
                newName: "Grade");
        }
    }
}
