using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppGimnasioMVC.Migrations
{
    public partial class Inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IngresoGimnasio",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaIngreso = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Bloqueado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngresoGimnasio", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Mensualidad",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ValorMensualidad = table.Column<double>(type: "float", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Bloqueado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mensualidad", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rutina",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ejercicio1 = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false),
                    Ejercicio2 = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true),
                    Ejercicio3 = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true),
                    Ejercicio4 = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true),
                    Ejercicio5 = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true),
                    Cardio = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true),
                    Bloqueado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rutina", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Persona",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumeroIdentificacion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TipoDocIdentificacion = table.Column<int>(type: "int", nullable: false),
                    Nombres = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Apellidos = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Contrasenia = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Celular = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    FechaNacimiento = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Genero = table.Column<int>(type: "int", nullable: false),
                    Rol = table.Column<int>(type: "int", nullable: false),
                    Bloqueado = table.Column<bool>(type: "bit", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Peso = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IngresoGimnasioId = table.Column<int>(type: "int", nullable: true),
                    MensualidadId = table.Column<int>(type: "int", nullable: true),
                    RutinaId = table.Column<int>(type: "int", nullable: true),
                    NumeroCuenta = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persona", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Persona_IngresoGimnasio_IngresoGimnasioId",
                        column: x => x.IngresoGimnasioId,
                        principalTable: "IngresoGimnasio",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Persona_Mensualidad_MensualidadId",
                        column: x => x.MensualidadId,
                        principalTable: "Mensualidad",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Persona_Rutina_RutinaId",
                        column: x => x.RutinaId,
                        principalTable: "Rutina",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Persona_IngresoGimnasioId",
                table: "Persona",
                column: "IngresoGimnasioId");

            migrationBuilder.CreateIndex(
                name: "IX_Persona_MensualidadId",
                table: "Persona",
                column: "MensualidadId");

            migrationBuilder.CreateIndex(
                name: "IX_Persona_RutinaId",
                table: "Persona",
                column: "RutinaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Persona");

            migrationBuilder.DropTable(
                name: "IngresoGimnasio");

            migrationBuilder.DropTable(
                name: "Mensualidad");

            migrationBuilder.DropTable(
                name: "Rutina");
        }
    }
}
