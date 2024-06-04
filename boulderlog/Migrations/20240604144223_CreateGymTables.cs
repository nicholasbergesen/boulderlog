using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Boulderlog.Migrations
{
    /// <inheritdoc />
    public partial class CreateGymTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Climb_Grade_GymId",
                table: "Climb");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Gym",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 36);

            migrationBuilder.AlterColumn<int>(
                name: "GymId",
                table: "Grade",
                type: "INTEGER",
                maxLength: 36,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 36);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Grade",
                type: "INTEGER",
                maxLength: 36,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 36);

            migrationBuilder.AlterColumn<int>(
                name: "GymId",
                table: "Climb",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 36);

            migrationBuilder.AlterColumn<int>(
                name: "GradeId",
                table: "Climb",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 36);

            migrationBuilder.InsertData(
                table: "Gym",
                columns: new[] { "Id", "Name", "Walls" },
                values: new object[,]
                {
                    { 1, "TheClimb-B-Hongdae", "Sector1;Sector2" },
                    { 2, "TheClimb-Yeonnam", "Yeonnam;Toitmaru;Sinchon" }
                });

            migrationBuilder.InsertData(
                table: "Grade",
                columns: new[] { "Id", "ColorHex", "ColorName", "GymId", "SortOrder", "VScale" },
                values: new object[,]
                {
                    { 1, "#FFFFFF", "1", 1, 1, "V0" },
                    { 2, "#FFFF00", "2", 1, 2, "V0" },
                    { 3, "#FFA500", "3", 1, 3, "V1" },
                    { 4, "#008000", "4", 1, 4, "V1" },
                    { 5, "#0000FF", "5", 1, 5, "V2" },
                    { 6, "#FF0000", "6", 1, 6, "V2" },
                    { 7, "#800080", "7", 1, 7, "V3" },
                    { 8, "#808080", "8", 1, 8, "V4" },
                    { 9, "#A52A2A", "9", 1, 9, "V5" },
                    { 10, "#FFFFFF", "White", 2, 1, "V0" },
                    { 11, "#FFFF00", "Yellow", 2, 2, "V1" },
                    { 12, "#FFA500", "Orange", 2, 3, "V2" },
                    { 13, "#008000", "Green", 2, 4, "V3" },
                    { 14, "#0000FF", "Blue", 2, 5, "V4" },
                    { 15, "#FF0000", "Red", 2, 6, "V5" },
                    { 16, "#800080", "Purple", 2, 7, "V6" },
                    { 17, "#808080", "Grey", 2, 8, "V7" },
                    { 18, "#A52A2A", "Brown", 2, 9, "V8" },
                    { 19, "#000000", "Black", 2, 10, "V9" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Climb_Gym_GymId",
                table: "Climb",
                column: "GymId",
                principalTable: "Gym",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Climb_Gym_GymId",
                table: "Climb");

            migrationBuilder.DeleteData(
                table: "Grade",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Grade",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Grade",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Grade",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Grade",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Grade",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Grade",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Grade",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Grade",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Grade",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Grade",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Grade",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Grade",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Grade",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Grade",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Grade",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Grade",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Grade",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Grade",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Gym",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Gym",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Gym",
                type: "TEXT",
                maxLength: 36,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<string>(
                name: "GymId",
                table: "Grade",
                type: "TEXT",
                maxLength: 36,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldMaxLength: 36);

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Grade",
                type: "TEXT",
                maxLength: 36,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldMaxLength: 36);

            migrationBuilder.AlterColumn<string>(
                name: "GymId",
                table: "Climb",
                type: "TEXT",
                maxLength: 36,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<string>(
                name: "GradeId",
                table: "Climb",
                type: "TEXT",
                maxLength: 36,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_Climb_Grade_GymId",
                table: "Climb",
                column: "GymId",
                principalTable: "Grade",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
