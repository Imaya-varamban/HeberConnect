using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HeberConnect.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddAssignmentAndStudentAssignmentFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentAssignments_Assignments_AssignmentId",
                table: "StudentAssignments");

            migrationBuilder.DropIndex(
                name: "IX_StudentAssignments_AssignmentId",
                table: "StudentAssignments");

            migrationBuilder.AddColumn<bool>(
                name: "IsSubmitted",
                table: "StudentAssignments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Assignments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSubmitted",
                table: "StudentAssignments");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Assignments");

            migrationBuilder.CreateIndex(
                name: "IX_StudentAssignments_AssignmentId",
                table: "StudentAssignments",
                column: "AssignmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentAssignments_Assignments_AssignmentId",
                table: "StudentAssignments",
                column: "AssignmentId",
                principalTable: "Assignments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
