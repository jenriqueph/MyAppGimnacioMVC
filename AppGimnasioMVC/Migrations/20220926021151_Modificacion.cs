using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppGimnasioMVC.Migrations
{
    public partial class Modificacion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Persona_IngresoGimnasio_IngresoGimnasioId",
                table: "Persona");

            migrationBuilder.DropForeignKey(
                name: "FK_Persona_Mensualidad_MensualidadId",
                table: "Persona");

            migrationBuilder.DropForeignKey(
                name: "FK_Persona_Rutina_RutinaId",
                table: "Persona");

            migrationBuilder.AddForeignKey(
                name: "FK_Persona_IngresoGimnasio_IngresoGimnasioId",
                table: "Persona",
                column: "IngresoGimnasioId",
                principalTable: "IngresoGimnasio",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Persona_Mensualidad_MensualidadId",
                table: "Persona",
                column: "MensualidadId",
                principalTable: "Mensualidad",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Persona_Rutina_RutinaId",
                table: "Persona",
                column: "RutinaId",
                principalTable: "Rutina",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Persona_IngresoGimnasio_IngresoGimnasioId",
                table: "Persona");

            migrationBuilder.DropForeignKey(
                name: "FK_Persona_Mensualidad_MensualidadId",
                table: "Persona");

            migrationBuilder.DropForeignKey(
                name: "FK_Persona_Rutina_RutinaId",
                table: "Persona");

            migrationBuilder.AddForeignKey(
                name: "FK_Persona_IngresoGimnasio_IngresoGimnasioId",
                table: "Persona",
                column: "IngresoGimnasioId",
                principalTable: "IngresoGimnasio",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Persona_Mensualidad_MensualidadId",
                table: "Persona",
                column: "MensualidadId",
                principalTable: "Mensualidad",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Persona_Rutina_RutinaId",
                table: "Persona",
                column: "RutinaId",
                principalTable: "Rutina",
                principalColumn: "Id");
        }
    }
}
