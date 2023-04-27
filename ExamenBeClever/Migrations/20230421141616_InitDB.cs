using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExamenBeClever.Migrations
{
    public partial class InitDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BusinessLocations",
                columns: table => new
                {
                    IdBusinessLocation = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessLocations", x => x.IdBusinessLocation);
                });

            migrationBuilder.CreateTable(
                name: "Empleados",
                columns: table => new
                {
                    IdEmpleado = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BusinessLocationIdBusinessLocation = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empleados", x => x.IdEmpleado);
                    table.ForeignKey(
                        name: "FK_Empleados_BusinessLocations_BusinessLocationIdBusinessLocation",
                        column: x => x.BusinessLocationIdBusinessLocation,
                        principalTable: "BusinessLocations",
                        principalColumn: "IdBusinessLocation");
                });

            migrationBuilder.CreateTable(
                name: "Registros",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdEmpleado = table.Column<int>(type: "int", nullable: false),
                    IdBusinessLocation = table.Column<int>(type: "int", nullable: false),
                    RegisterType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Registros", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Registros_BusinessLocations_IdBusinessLocation",
                        column: x => x.IdBusinessLocation,
                        principalTable: "BusinessLocations",
                        principalColumn: "IdBusinessLocation",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Registros_Empleados_IdEmpleado",
                        column: x => x.IdEmpleado,
                        principalTable: "Empleados",
                        principalColumn: "IdEmpleado",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Empleados_BusinessLocationIdBusinessLocation",
                table: "Empleados",
                column: "BusinessLocationIdBusinessLocation");

            migrationBuilder.CreateIndex(
                name: "IX_Registros_IdBusinessLocation",
                table: "Registros",
                column: "IdBusinessLocation");

            migrationBuilder.CreateIndex(
                name: "IX_Registros_IdEmpleado",
                table: "Registros",
                column: "IdEmpleado");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Registros");

            migrationBuilder.DropTable(
                name: "Empleados");

            migrationBuilder.DropTable(
                name: "BusinessLocations");
        }
    }
}
