using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppGimnasioMVC.Migrations
{
    public partial class Ingreso : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PersonaId",
                table: "Mensualidad");

            migrationBuilder.DropColumn(
                name: "PersonaId",
                table: "IngresoGimnasio");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PersonaId",
                table: "Mensualidad",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PersonaId",
                table: "IngresoGimnasio",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
