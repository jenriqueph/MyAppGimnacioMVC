using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppGimnasioMVC.Migrations
{
    public partial class Mensualidad : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Persona_Mensualidad_MensualidadId",
                table: "Persona");

            migrationBuilder.DropIndex(
                name: "IX_Persona_MensualidadId",
                table: "Persona");

            migrationBuilder.DropColumn(
                name: "MensualidadId",
                table: "Persona");

            migrationBuilder.AlterColumn<string>(
                name: "NombreRutina",
                table: "Rutina",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "Codigo",
                table: "Rutina",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AddColumn<int>(
                name: "ClienteId",
                table: "Mensualidad",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.CreateIndex(
                name: "IX_Mensualidad_ClienteId",
                table: "Mensualidad",
                column: "ClienteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Mensualidad_Persona_ClienteId",
                table: "Mensualidad",
                column: "ClienteId",
                principalTable: "Persona",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mensualidad_Persona_ClienteId",
                table: "Mensualidad");

            migrationBuilder.DropIndex(
                name: "IX_Mensualidad_ClienteId",
                table: "Mensualidad");

            migrationBuilder.DropColumn(
                name: "ClienteId",
                table: "Mensualidad");

            migrationBuilder.DropColumn(
                name: "PersonaId",
                table: "Mensualidad");

            migrationBuilder.DropColumn(
                name: "PersonaId",
                table: "IngresoGimnasio");

            migrationBuilder.AlterColumn<string>(
                name: "NombreRutina",
                table: "Rutina",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Codigo",
                table: "Rutina",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(40)",
                oldMaxLength: 40);

            migrationBuilder.AddColumn<int>(
                name: "MensualidadId",
                table: "Persona",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Persona_MensualidadId",
                table: "Persona",
                column: "MensualidadId");

            migrationBuilder.AddForeignKey(
                name: "FK_Persona_Mensualidad_MensualidadId",
                table: "Persona",
                column: "MensualidadId",
                principalTable: "Mensualidad",
                principalColumn: "Id");
        }
    }
}
