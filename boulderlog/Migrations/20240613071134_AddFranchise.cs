using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Boulderlog.Migrations
{
    /// <inheritdoc />
    public partial class AddFranchise : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Grade_Gym_GymId",
                table: "Grade");

            migrationBuilder.DropIndex(
                name: "IX_Grade_GymId",
                table: "Grade");

            migrationBuilder.DropColumn(
                name: "GymId",
                table: "Grade");

            migrationBuilder.AddColumn<int>(
                name: "FranchiseId",
                table: "Gym",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FranchiseId",
                table: "Grade",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FranchiseId",
                table: "Climb",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Franchise",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Franchise", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Franchise",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "TheClimb-B" },
                    { 2, "TheClimb" }
                });

            migrationBuilder.UpdateData(
                table: "Grade",
                keyColumn: "Id",
                keyValue: 1,
                column: "FranchiseId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Grade",
                keyColumn: "Id",
                keyValue: 2,
                column: "FranchiseId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Grade",
                keyColumn: "Id",
                keyValue: 3,
                column: "FranchiseId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Grade",
                keyColumn: "Id",
                keyValue: 4,
                column: "FranchiseId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Grade",
                keyColumn: "Id",
                keyValue: 5,
                column: "FranchiseId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Grade",
                keyColumn: "Id",
                keyValue: 6,
                column: "FranchiseId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Grade",
                keyColumn: "Id",
                keyValue: 7,
                column: "FranchiseId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Grade",
                keyColumn: "Id",
                keyValue: 8,
                column: "FranchiseId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Grade",
                keyColumn: "Id",
                keyValue: 9,
                column: "FranchiseId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Grade",
                keyColumn: "Id",
                keyValue: 10,
                column: "FranchiseId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Grade",
                keyColumn: "Id",
                keyValue: 11,
                column: "FranchiseId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Grade",
                keyColumn: "Id",
                keyValue: 12,
                column: "FranchiseId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Grade",
                keyColumn: "Id",
                keyValue: 13,
                column: "FranchiseId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Grade",
                keyColumn: "Id",
                keyValue: 14,
                column: "FranchiseId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Grade",
                keyColumn: "Id",
                keyValue: 15,
                column: "FranchiseId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Grade",
                keyColumn: "Id",
                keyValue: 16,
                column: "FranchiseId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Grade",
                keyColumn: "Id",
                keyValue: 17,
                column: "FranchiseId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Grade",
                keyColumn: "Id",
                keyValue: 18,
                column: "FranchiseId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Grade",
                keyColumn: "Id",
                keyValue: 19,
                column: "FranchiseId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Gym",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FranchiseId", "Name" },
                values: new object[] { 1, "Hongdae" });

            migrationBuilder.UpdateData(
                table: "Gym",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FranchiseId", "Name" },
                values: new object[] { 2, "Yeonnam" });

            migrationBuilder.CreateIndex(
                name: "IX_Gym_FranchiseId",
                table: "Gym",
                column: "FranchiseId");

            migrationBuilder.CreateIndex(
                name: "IX_Grade_FranchiseId",
                table: "Grade",
                column: "FranchiseId");

            migrationBuilder.CreateIndex(
                name: "IX_Climb_FranchiseId",
                table: "Climb",
                column: "FranchiseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Climb_Franchise_FranchiseId",
                table: "Climb",
                column: "FranchiseId",
                principalTable: "Franchise",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Grade_Franchise_FranchiseId",
                table: "Grade",
                column: "FranchiseId",
                principalTable: "Franchise",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Gym_Franchise_FranchiseId",
                table: "Gym",
                column: "FranchiseId",
                principalTable: "Franchise",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Climb_Franchise_FranchiseId",
                table: "Climb");

            migrationBuilder.DropForeignKey(
                name: "FK_Grade_Franchise_FranchiseId",
                table: "Grade");

            migrationBuilder.DropForeignKey(
                name: "FK_Gym_Franchise_FranchiseId",
                table: "Gym");

            migrationBuilder.DropTable(
                name: "Franchise");

            migrationBuilder.DropIndex(
                name: "IX_Gym_FranchiseId",
                table: "Gym");

            migrationBuilder.DropIndex(
                name: "IX_Grade_FranchiseId",
                table: "Grade");

            migrationBuilder.DropIndex(
                name: "IX_Climb_FranchiseId",
                table: "Climb");

            migrationBuilder.DropColumn(
                name: "FranchiseId",
                table: "Gym");

            migrationBuilder.DropColumn(
                name: "FranchiseId",
                table: "Grade");

            migrationBuilder.DropColumn(
                name: "FranchiseId",
                table: "Climb");

            migrationBuilder.AddColumn<int>(
                name: "GymId",
                table: "Grade",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Grade",
                keyColumn: "Id",
                keyValue: 1,
                column: "GymId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Grade",
                keyColumn: "Id",
                keyValue: 2,
                column: "GymId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Grade",
                keyColumn: "Id",
                keyValue: 3,
                column: "GymId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Grade",
                keyColumn: "Id",
                keyValue: 4,
                column: "GymId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Grade",
                keyColumn: "Id",
                keyValue: 5,
                column: "GymId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Grade",
                keyColumn: "Id",
                keyValue: 6,
                column: "GymId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Grade",
                keyColumn: "Id",
                keyValue: 7,
                column: "GymId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Grade",
                keyColumn: "Id",
                keyValue: 8,
                column: "GymId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Grade",
                keyColumn: "Id",
                keyValue: 9,
                column: "GymId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Grade",
                keyColumn: "Id",
                keyValue: 10,
                column: "GymId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Grade",
                keyColumn: "Id",
                keyValue: 11,
                column: "GymId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Grade",
                keyColumn: "Id",
                keyValue: 12,
                column: "GymId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Grade",
                keyColumn: "Id",
                keyValue: 13,
                column: "GymId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Grade",
                keyColumn: "Id",
                keyValue: 14,
                column: "GymId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Grade",
                keyColumn: "Id",
                keyValue: 15,
                column: "GymId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Grade",
                keyColumn: "Id",
                keyValue: 16,
                column: "GymId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Grade",
                keyColumn: "Id",
                keyValue: 17,
                column: "GymId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Grade",
                keyColumn: "Id",
                keyValue: 18,
                column: "GymId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Grade",
                keyColumn: "Id",
                keyValue: 19,
                column: "GymId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Gym",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "TheClimb-B-Hongdae");

            migrationBuilder.UpdateData(
                table: "Gym",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "TheClimb-Yeonnam");

            migrationBuilder.CreateIndex(
                name: "IX_Grade_GymId",
                table: "Grade",
                column: "GymId");

            migrationBuilder.AddForeignKey(
                name: "FK_Grade_Gym_GymId",
                table: "Grade",
                column: "GymId",
                principalTable: "Gym",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
