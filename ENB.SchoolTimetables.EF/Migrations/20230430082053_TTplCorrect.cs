using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ENB.SchoolTimetables.EF.Migrations
{
    /// <inheritdoc />
    public partial class TTplCorrect : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlannedTimetable_Subjects_SubjectId",
                table: "PlannedTimetable");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "063da1d4-a8f9-4bdc-b7a4-7124884c31dc");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5649b3da-368d-41da-abb0-061b046b97a7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "923d5743-b111-4741-adf6-b57d7d3bee04");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e1fd9b24-3a4a-4cf5-8391-3f597a6b5843");

            migrationBuilder.DropColumn(
                name: "SujectId",
                table: "PlannedTimetable");

            migrationBuilder.AlterColumn<int>(
                name: "SubjectId",
                table: "PlannedTimetable",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4d27a28c-0c44-4fd7-895b-534f0f48c1c3", null, "Visitor", "VISITOR" },
                    { "a92b4754-3a27-4cb6-beb9-2cdfa3c612a3", null, "Administrator", "ADMINISTRATOR" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_PlannedTimetable_Subjects_SubjectId",
                table: "PlannedTimetable",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlannedTimetable_Subjects_SubjectId",
                table: "PlannedTimetable");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4d27a28c-0c44-4fd7-895b-534f0f48c1c3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a92b4754-3a27-4cb6-beb9-2cdfa3c612a3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dff5dbf7-4028-4ff2-908b-f48d4853bc2d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fc601b3e-9adb-4bcd-9a80-ae632b4a860b");

            migrationBuilder.AlterColumn<int>(
                name: "SubjectId",
                table: "PlannedTimetable",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "SujectId",
                table: "PlannedTimetable",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "063da1d4-a8f9-4bdc-b7a4-7124884c31dc", null, "Visitor", "VISITOR" },
                    { "5649b3da-368d-41da-abb0-061b046b97a7", null, "Administrator", "ADMINISTRATOR" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_PlannedTimetable_Subjects_SubjectId",
                table: "PlannedTimetable",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id");
        }
    }
}
