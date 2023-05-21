using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ENB.SchoolTimetables.EF.Migrations
{
    /// <inheritdoc />
    public partial class PlndTT : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0656716f-1122-472b-aa35-d936ff5fd4c3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "15c55f34-5a40-44f4-897e-3943b8bd9a65");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2b21ccdd-9152-4065-b879-aaf8428ba9bd");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bdec6cff-a1fc-475c-bf46-a2a3470f1486");

            migrationBuilder.AddColumn<int>(
                name: "TeacherId",
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

            migrationBuilder.CreateIndex(
                name: "IX_PlannedTimetable_TeacherId",
                table: "PlannedTimetable",
                column: "TeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_PlannedTimetable_Teachers_TeacherId",
                table: "PlannedTimetable",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlannedTimetable_Teachers_TeacherId",
                table: "PlannedTimetable");

            migrationBuilder.DropIndex(
                name: "IX_PlannedTimetable_TeacherId",
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
                name: "TeacherId",
                table: "PlannedTimetable");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0656716f-1122-472b-aa35-d936ff5fd4c3", null, "Visitor", "VISITOR" },
                    { "15c55f34-5a40-44f4-897e-3943b8bd9a65", null, "Administrator", "ADMINISTRATOR" }
                });
        }
    }
}
