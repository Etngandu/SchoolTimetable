using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ENB.SchoolTimetables.EF.Migrations
{
    /// <inheritdoc />
    public partial class statusTT : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<int>(
                name: "TimetableStatus",
                table: "PlannedTimetable",
                type: "int",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "01415909-b838-4c2f-b3f1-1a878d829712", null, "Visitor", "VISITOR" },
                    { "359867a7-cb83-40c4-93e3-3f0f9fbb89f8", null, "Administrator", "ADMINISTRATOR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "01415909-b838-4c2f-b3f1-1a878d829712");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "359867a7-cb83-40c4-93e3-3f0f9fbb89f8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "46047901-0258-4363-9041-bc30af1e8a38");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ad476329-25ba-4768-bf1e-538edbf08932");

            migrationBuilder.DropColumn(
                name: "TimetableStatus",
                table: "PlannedTimetable");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "d0dbe7d1-f4ba-4ff6-a200-13aac6cc47e3", null, "Administrator", "ADMINISTRATOR" },
                    { "d1bb2f1b-9cf8-4663-91c3-538437fe77cf", null, "Visitor", "VISITOR" }
                });
        }
    }
}
