using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExamenBeClever.Migrations
{
    public partial class AgregamosGeneroAEmpleado : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Genero",
                table: "Empleados",
                type: "nvarchar(1)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Genero",
                table: "Empleados");
        }
    }
}
