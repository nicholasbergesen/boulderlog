using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Boulderlog.Migrations
{
    /// <inheritdoc />
    public partial class MoveUserId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Climb_AspNetUsers_UserId",
                table: "Climb");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Climb",
                newName: "CreatedByUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Climb_UserId",
                table: "Climb",
                newName: "IX_Climb_CreatedByUserId");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "ClimbLog",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ClimbLog_UserId",
                table: "ClimbLog",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Climb_AspNetUsers_CreatedByUserId",
                table: "Climb",
                column: "CreatedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClimbLog_AspNetUsers_UserId",
                table: "ClimbLog",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Climb_AspNetUsers_CreatedByUserId",
                table: "Climb");

            migrationBuilder.DropForeignKey(
                name: "FK_ClimbLog_AspNetUsers_UserId",
                table: "ClimbLog");

            migrationBuilder.DropIndex(
                name: "IX_ClimbLog_UserId",
                table: "ClimbLog");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ClimbLog");

            migrationBuilder.RenameColumn(
                name: "CreatedByUserId",
                table: "Climb",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Climb_CreatedByUserId",
                table: "Climb",
                newName: "IX_Climb_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Climb_AspNetUsers_UserId",
                table: "Climb",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
