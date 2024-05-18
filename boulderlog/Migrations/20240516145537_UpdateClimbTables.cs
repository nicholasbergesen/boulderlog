using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace boulderlog.Migrations
{
    /// <inheritdoc />
    public partial class UpdateClimbTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClimbLog_Climb_ClimbId",
                table: "ClimbLog");

            migrationBuilder.AlterColumn<string>(
                name: "ClimbId",
                table: "ClimbLog",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Climb",
                type: "TEXT",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Climb",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Climb_UserId",
                table: "Climb",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Climb_AspNetUsers_UserId",
                table: "Climb",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClimbLog_Climb_ClimbId",
                table: "ClimbLog",
                column: "ClimbId",
                principalTable: "Climb",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Climb_AspNetUsers_UserId",
                table: "Climb");

            migrationBuilder.DropForeignKey(
                name: "FK_ClimbLog_Climb_ClimbId",
                table: "ClimbLog");

            migrationBuilder.DropIndex(
                name: "IX_Climb_UserId",
                table: "Climb");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Climb");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Climb");

            migrationBuilder.AlterColumn<string>(
                name: "ClimbId",
                table: "ClimbLog",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddForeignKey(
                name: "FK_ClimbLog_Climb_ClimbId",
                table: "ClimbLog",
                column: "ClimbId",
                principalTable: "Climb",
                principalColumn: "Id");
        }
    }
}
