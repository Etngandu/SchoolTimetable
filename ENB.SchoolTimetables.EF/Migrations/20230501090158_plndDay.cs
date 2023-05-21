using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ENB.SchoolTimetables.EF.Migrations
{
    /// <inheritdoc />
    public partial class plndDay : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AddColumn<DateTime>(
                name: "PlanneddDay",
                table: "PlannedTimetable",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "78d0b1a3-beef-448e-a791-37d8e2d1a5c5", null, "Visitor", "VISITOR" },
                    { "aba262bc-25de-4848-83b5-e6374f09346e", null, "Administrator", "ADMINISTRATOR" },
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "78d0b1a3-beef-448e-a791-37d8e2d1a5c5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "aba262bc-25de-4848-83b5-e6374f09346e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cdb7083e-0b14-4aee-84d4-a9df582d414d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ebdc250f-09a8-48e9-a720-668f0d4f5c9c");

            migrationBuilder.DropColumn(
                name: "PlanneddDay",
                table: "PlannedTimetable");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4d27a28c-0c44-4fd7-895b-534f0f48c1c3", null, "Visitor", "VISITOR" },
                    { "a92b4754-3a27-4cb6-beb9-2cdfa3c612a3", null, "Administrator", "ADMINISTRATOR" }
                });
        }
    }
}
