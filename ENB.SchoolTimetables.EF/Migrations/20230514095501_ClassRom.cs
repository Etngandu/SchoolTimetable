using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ENB.SchoolTimetables.EF.Migrations
{
    /// <inheritdoc />
    public partial class ClassRom : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0f2729ed-2216-4e2d-96da-3750effb8f24");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "75c04c69-e3b6-4e00-b5ec-c1e0009f1481");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b6be3cef-9a9c-4bba-83bf-1ac7f123360a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fe9b5bb7-a935-4f71-b121-27b2714719e5");

            migrationBuilder.DropColumn(
                name: "PeriodNumber",
                table: "GeneratedTimetable");

            migrationBuilder.RenameColumn(
                name: "RefClassR",
                table: "classRs",
                newName: "Classroom");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "78ed19b1-245b-408f-b232-7ff6645bcd8e", null, "Visitor", "VISITOR" },
                    { "e30562a6-b945-4748-9dc6-26d4950f5320", null, "Administrator", "ADMINISTRATOR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "78ed19b1-245b-408f-b232-7ff6645bcd8e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e30562a6-b945-4748-9dc6-26d4950f5320");

            migrationBuilder.RenameColumn(
                name: "Classroom",
                table: "classRs",
                newName: "RefClassR");

            migrationBuilder.AddColumn<int>(
                name: "PeriodNumber",
                table: "GeneratedTimetable",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0f2729ed-2216-4e2d-96da-3750effb8f24", null, "Administrator", "ADMINISTRATOR" },
                    { "fe9b5bb7-a935-4f71-b121-27b2714719e5", null, "Visitor", "VISITOR" }
                });
        }
    }
}
