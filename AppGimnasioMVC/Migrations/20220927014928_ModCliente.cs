using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppGimnasioMVC.Migrations
{
    public partial class ModCliente : Migration
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

            migrationBuilder.DropIndex(
                name: "IX_Persona_IngresoGimnasioId",
                table: "Persona");

            migrationBuilder.DropColumn(
                name: "IngresoGimnasioId",
                table: "Persona");

            migrationBuilder.AddColumn<int>(
                name: "ClienteId",
                table: "IngresoGimnasio",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_IngresoGimnasio_ClienteId",
                table: "IngresoGimnasio",
                column: "ClienteId");

            migrationBuilder.AddForeignKey(
                name: "FK_IngresoGimnasio_Persona_ClienteId",
                table: "IngresoGimnasio",
                column: "ClienteId",
                principalTable: "Persona",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IngresoGimnasio_Persona_ClienteId",
                table: "IngresoGimnasio");

            migrationBuilder.DropForeignKey(
                name: "FK_Persona_Mensualidad_MensualidadId",
                table: "Persona");

            migrationBuilder.DropForeignKey(
                name: "FK_Persona_Rutina_RutinaId",
                table: "Persona");

            migrationBuilder.DropIndex(
                name: "IX_IngresoGimnasio_ClienteId",
                table: "IngresoGimnasio");

            migrationBuilder.DropColumn(
                name: "ClienteId",
                table: "IngresoGimnasio");

            migrationBuilder.AddColumn<int>(
                name: "IngresoGimnasioId",
                table: "Persona",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Persona_IngresoGimnasioId",
                table: "Persona",
                column: "IngresoGimnasioId");

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
    }
}
