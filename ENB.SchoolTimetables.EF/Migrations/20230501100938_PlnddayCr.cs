using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ENB.SchoolTimetables.EF.Migrations
{
    /// <inheritdoc />
    public partial class PlnddayCr : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0153f1ce-921d-4286-8549-cfcbd33f0c2a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9a73d537-88da-4d96-9e38-f29249373f6f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a870bde3-5c89-4246-84a3-d66c5feebc55");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d7475ef1-2a5a-40fe-95f1-0482b643b68d");

            migrationBuilder.RenameColumn(
                name: "PlanneddDay",
                table: "PlannedTimetable",
                newName: "PlannedDay");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "d0dbe7d1-f4ba-4ff6-a200-13aac6cc47e3", null, "Administrator", "ADMINISTRATOR" },
                    { "d1bb2f1b-9cf8-4663-91c3-538437fe77cf", null, "Visitor", "VISITOR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d0dbe7d1-f4ba-4ff6-a200-13aac6cc47e3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d1bb2f1b-9cf8-4663-91c3-538437fe77cf");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e9d4370f-a286-481c-b874-d144bcba7836");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ff3d92d8-267e-43ac-bedd-0b5af81ce7b7");

            migrationBuilder.RenameColumn(
                name: "PlannedDay",
                table: "PlannedTimetable",
                newName: "PlanneddDay");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0153f1ce-921d-4286-8549-cfcbd33f0c2a", null, "Administrator", "ADMINISTRATOR" },
                    { "9a73d537-88da-4d96-9e38-f29249373f6f", null, "Visitor", "VISITOR" }
                });
        }
    }
}
