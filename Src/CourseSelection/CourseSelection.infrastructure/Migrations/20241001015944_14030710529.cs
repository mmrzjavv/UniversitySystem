using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourseSelection.infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _14030710529 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Units_SelectionTime_SelectionTimeId",
                table: "Units");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SelectionTime",
                table: "SelectionTime");

            migrationBuilder.RenameTable(
                name: "SelectionTime",
                newName: "SelectionTimes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SelectionTimes",
                table: "SelectionTimes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Units_SelectionTimes_SelectionTimeId",
                table: "Units",
                column: "SelectionTimeId",
                principalTable: "SelectionTimes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Units_SelectionTimes_SelectionTimeId",
                table: "Units");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SelectionTimes",
                table: "SelectionTimes");

            migrationBuilder.RenameTable(
                name: "SelectionTimes",
                newName: "SelectionTime");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SelectionTime",
                table: "SelectionTime",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Units_SelectionTime_SelectionTimeId",
                table: "Units",
                column: "SelectionTimeId",
                principalTable: "SelectionTime",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
