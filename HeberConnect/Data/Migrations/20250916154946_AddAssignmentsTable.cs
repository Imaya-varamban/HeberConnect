using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HeberConnect.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddAssignmentsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSubmitted",
                table: "StudentAssignments");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsSubmitted",
                table: "StudentAssignments",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
