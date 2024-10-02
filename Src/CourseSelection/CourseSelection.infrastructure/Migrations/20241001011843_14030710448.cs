using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourseSelection.infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _14030710448 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Units_Courses_CourseId",
                table: "Units");

            migrationBuilder.DropForeignKey(
                name: "FK_Units_Courses_CoursesId",
                table: "Units");

            migrationBuilder.DropTable(
                name: "ReserveUnits",
                schema: "UnitsSelection");

            migrationBuilder.DropTable(
                name: "Units",
                schema: "UnitsSelection");

            migrationBuilder.DropTable(
                name: "Courses",
                schema: "UnitsSelection");

            migrationBuilder.DropIndex(
                name: "IX_Units_CourseId",
                table: "Units");

            migrationBuilder.DropIndex(
                name: "IX_Units_CoursesId",
                table: "Units");

            migrationBuilder.DropColumn(
                name: "CoursesId",
                table: "Units");

            migrationBuilder.AlterColumn<double>(
                name: "Price",
                table: "Units",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddForeignKey(
                name: "FK_Units_Courses_Id",
                table: "Units",
                column: "Id",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Units_Courses_Id",
                table: "Units");

            migrationBuilder.EnsureSchema(
                name: "UnitsSelection");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Units",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AddColumn<Guid>(
                name: "CoursesId",
                table: "Units",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Courses",
                schema: "UnitsSelection",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2(6)", nullable: false),
                    ModifyDate = table.Column<DateTime>(type: "datetime2(6)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UnitsCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Units",
                schema: "UnitsSelection",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CourseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AllCapacity = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2(6)", nullable: false),
                    ExamTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifyDate = table.Column<DateTime>(type: "datetime2(6)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ReservedCapacity = table.Column<int>(type: "int", nullable: false),
                    Teacher = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Units", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Units_Courses_CourseId",
                        column: x => x.CourseId,
                        principalSchema: "UnitsSelection",
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReserveUnits",
                schema: "UnitsSelection",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UnitsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2(6)", nullable: false),
                    ModifyDate = table.Column<DateTime>(type: "datetime2(6)", nullable: true),
                    UnitId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReserveUnits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReserveUnits_Units_UnitsId",
                        column: x => x.UnitsId,
                        principalSchema: "UnitsSelection",
                        principalTable: "Units",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Units_CourseId",
                table: "Units",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Units_CoursesId",
                table: "Units",
                column: "CoursesId");

            migrationBuilder.CreateIndex(
                name: "IX_ReserveUnits_UnitsId",
                schema: "UnitsSelection",
                table: "ReserveUnits",
                column: "UnitsId");

            migrationBuilder.CreateIndex(
                name: "IX_Units_CourseId",
                schema: "UnitsSelection",
                table: "Units",
                column: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Units_Courses_CourseId",
                table: "Units",
                column: "CourseId",
                principalSchema: "UnitsSelection",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Units_Courses_CoursesId",
                table: "Units",
                column: "CoursesId",
                principalTable: "Courses",
                principalColumn: "Id");
        }
    }
}
