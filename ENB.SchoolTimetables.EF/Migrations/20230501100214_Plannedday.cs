using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ENB.SchoolTimetables.EF.Migrations
{
    /// <inheritdoc />
    public partial class Plannedday : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "Day_Name",
                table: "PlannedTimetable");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0153f1ce-921d-4286-8549-cfcbd33f0c2a", null, "Administrator", "ADMINISTRATOR" },
                    { "9a73d537-88da-4d96-9e38-f29249373f6f", null, "Visitor", "VISITOR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<int>(
                name: "Day_Name",
                table: "PlannedTimetable",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "78d0b1a3-beef-448e-a791-37d8e2d1a5c5", null, "Visitor", "VISITOR" },
                    { "aba262bc-25de-4848-83b5-e6374f09346e", null, "Administrator", "ADMINISTRATOR" }
                });
        }
    }
}
