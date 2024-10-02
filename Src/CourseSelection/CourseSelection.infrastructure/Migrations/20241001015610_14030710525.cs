using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourseSelection.infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _14030710525 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "SelectionTimeId",
                table: "Units",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "SelectionTime",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TimeName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2(6)", nullable: false),
                    ModifyDate = table.Column<DateTime>(type: "datetime2(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SelectionTime", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Units_SelectionTimeId",
                table: "Units",
                column: "SelectionTimeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Units_SelectionTime_SelectionTimeId",
                table: "Units",
                column: "SelectionTimeId",
                principalTable: "SelectionTime",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Units_SelectionTime_SelectionTimeId",
                table: "Units");

            migrationBuilder.DropTable(
                name: "SelectionTime");

            migrationBuilder.DropIndex(
                name: "IX_Units_SelectionTimeId",
                table: "Units");

            migrationBuilder.DropColumn(
                name: "SelectionTimeId",
                table: "Units");
        }
    }
}
