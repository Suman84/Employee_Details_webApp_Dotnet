using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RepositoryLayer.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Peoples",
                columns: table => new
                {
                    Personid = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: true),
                    MiddleName = table.Column<string>(type: "text", nullable: true),
                    LastName = table.Column<string>(type: "text", nullable: true),
                    Address = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Peoples", x => x.Personid);
                });

            migrationBuilder.CreateTable(
                name: "Positions",
                columns: table => new
                {
                    Positionid = table.Column<Guid>(type: "uuid", nullable: false),
                    PositionName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Positions", x => x.Positionid);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Employeeid = table.Column<Guid>(type: "uuid", nullable: false),
                    Salary = table.Column<int>(type: "integer", nullable: false),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EmployeeCode = table.Column<int>(type: "integer", nullable: false),
                    ISDisabled = table.Column<bool>(type: "boolean", nullable: false),
                    Personid = table.Column<Guid>(type: "uuid", nullable: false),
                    Positionid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Employeeid);
                    table.ForeignKey(
                        name: "FK_Employees_Peoples_Personid",
                        column: x => x.Personid,
                        principalTable: "Peoples",
                        principalColumn: "Personid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Employees_Positions_Positionid",
                        column: x => x.Positionid,
                        principalTable: "Positions",
                        principalColumn: "Positionid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeJobHistories",
                columns: table => new
                {
                    EmployeeJobHistoryid = table.Column<Guid>(type: "uuid", nullable: false),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Employeeid = table.Column<Guid>(type: "uuid", nullable: false),
                    Positionid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeJobHistories", x => x.EmployeeJobHistoryid);
                    table.ForeignKey(
                        name: "FK_EmployeeJobHistories_Employees_Employeeid",
                        column: x => x.Employeeid,
                        principalTable: "Employees",
                        principalColumn: "Employeeid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeJobHistories_Positions_Positionid",
                        column: x => x.Positionid,
                        principalTable: "Positions",
                        principalColumn: "Positionid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeJobHistories_Employeeid",
                table: "EmployeeJobHistories",
                column: "Employeeid");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeJobHistories_Positionid",
                table: "EmployeeJobHistories",
                column: "Positionid");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_Personid",
                table: "Employees",
                column: "Personid");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_Positionid",
                table: "Employees",
                column: "Positionid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeJobHistories");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Peoples");

            migrationBuilder.DropTable(
                name: "Positions");
        }
    }
}
