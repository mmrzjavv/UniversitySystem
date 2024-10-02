using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourseSelection.infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "UnitsSelection");

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    UnitsCount = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2(6)", nullable: false),
                    ModifyDate = table.Column<DateTime>(type: "datetime2(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                schema: "UnitsSelection",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UnitsCount = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2(6)", nullable: false),
                    ModifyDate = table.Column<DateTime>(type: "datetime2(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Units",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false),
                    Teacher = table.Column<string>(type: "nvarchar(700)", maxLength: 700, nullable: false),
                    AllCapacity = table.Column<int>(type: "int", nullable: false),
                    ReservedCapacity = table.Column<int>(type: "int", nullable: false),
                    ExamTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CourseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CoursesId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2(6)", nullable: false),
                    ModifyDate = table.Column<DateTime>(type: "datetime2(6)", nullable: true)
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
                    table.ForeignKey(
                        name: "FK_Units_Courses_CoursesId",
                        column: x => x.CoursesId,
                        principalTable: "Courses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Units",
                schema: "UnitsSelection",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Teacher = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AllCapacity = table.Column<int>(type: "int", nullable: false),
                    ReservedCapacity = table.Column<int>(type: "int", nullable: false),
                    ExamTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CourseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2(6)", nullable: false),
                    ModifyDate = table.Column<DateTime>(type: "datetime2(6)", nullable: true)
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
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UnitId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2(6)", nullable: false),
                    ModifyDate = table.Column<DateTime>(type: "datetime2(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReserveUnits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReserveUnits_Units_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Units",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReserveUnits",
                schema: "UnitsSelection",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UnitId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UnitsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2(6)", nullable: false),
                    ModifyDate = table.Column<DateTime>(type: "datetime2(6)", nullable: true)
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
                name: "IX_ReserveUnits_UnitId",
                table: "ReserveUnits",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_ReserveUnits_UnitsId",
                schema: "UnitsSelection",
                table: "ReserveUnits",
                column: "UnitsId");

            migrationBuilder.CreateIndex(
                name: "IX_Units_CourseId",
                table: "Units",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Units_CoursesId",
                table: "Units",
                column: "CoursesId");

            migrationBuilder.CreateIndex(
                name: "IX_Units_CourseId",
                schema: "UnitsSelection",
                table: "Units",
                column: "CourseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReserveUnits");

            migrationBuilder.DropTable(
                name: "ReserveUnits",
                schema: "UnitsSelection");

            migrationBuilder.DropTable(
                name: "Units");

            migrationBuilder.DropTable(
                name: "Units",
                schema: "UnitsSelection");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "Courses",
                schema: "UnitsSelection");
        }
    }
}
