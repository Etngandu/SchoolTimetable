using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ENB.SchoolTimetables.EF.Migrations
{
    /// <inheritdoc />
    public partial class GenerateTT : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "GeneratedTimetable",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeacherId = table.Column<int>(type: "int", nullable: false),
                    PlannedTimetableId = table.Column<int>(type: "int", nullable: false),
                    SubjectId = table.Column<int>(type: "int", nullable: false),
                    ClassRId = table.Column<int>(type: "int", nullable: false),
                    PeriodNumber = table.Column<int>(type: "int", nullable: false),
                    Start = table.Column<DateTime>(type: "datetime2", nullable: false),
                    End = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Color = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeneratedTimetable", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GeneratedTimetable_PlannedTimetable_PlannedTimetableId",
                        column: x => x.PlannedTimetableId,
                        principalTable: "PlannedTimetable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GeneratedTimetable_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GeneratedTimetable_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GeneratedTimetable_classRs_ClassRId",
                        column: x => x.ClassRId,
                        principalTable: "classRs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0f2729ed-2216-4e2d-96da-3750effb8f24", null, "Administrator", "ADMINISTRATOR" },
                     { "d1bb2f1b-9cf8-4663-91c3-538437fe77cf", null, "Visitor", "VISITOR" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_GeneratedTimetable_ClassRId",
                table: "GeneratedTimetable",
                column: "ClassRId");

            migrationBuilder.CreateIndex(
                name: "IX_GeneratedTimetable_PlannedTimetableId",
                table: "GeneratedTimetable",
                column: "PlannedTimetableId");

            migrationBuilder.CreateIndex(
                name: "IX_GeneratedTimetable_SubjectId",
                table: "GeneratedTimetable",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_GeneratedTimetable_TeacherId",
                table: "GeneratedTimetable",
                column: "TeacherId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GeneratedTimetable");

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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "01415909-b838-4c2f-b3f1-1a878d829712", null, "Visitor", "VISITOR" },
                    { "359867a7-cb83-40c4-93e3-3f0f9fbb89f8", null, "Administrator", "ADMINISTRATOR" }
                });
        }
    }
}
