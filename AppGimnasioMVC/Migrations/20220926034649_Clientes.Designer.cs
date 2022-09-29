﻿// <auto-generated />
using System;
using AppGimnasioMVC.Datos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AppGimnasioMVC.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20220926034649_Clientes")]
    partial class Clientes
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("AppGimnasioMVC.Models.IngresoGimnasio", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("Bloqueado")
                        .HasColumnType("bit");

                    b.Property<DateTime>("FechaIngreso")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("IngresoGimnasio");
                });

            modelBuilder.Entity("AppGimnasioMVC.Models.Mensualidad", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("Bloqueado")
                        .HasColumnType("bit");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaFin")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaInicio")
                        .HasColumnType("datetime2");

                    b.Property<double>("ValorMensualidad")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Mensualidad");
                });

            modelBuilder.Entity("AppGimnasioMVC.Models.Persona", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Apellidos")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool>("Bloqueado")
                        .HasColumnType("bit");

                    b.Property<string>("Celular")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("Contrasenia")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Direccion")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("FechaNacimiento")
                        .HasColumnType("datetime2");

                    b.Property<int>("Genero")
                        .HasColumnType("int");

                    b.Property<string>("Nombres")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("NumeroIdentificacion")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("Rol")
                        .HasColumnType("int");

                    b.Property<int>("TipoDocIdentificacion")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Persona");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Persona");
                });

            modelBuilder.Entity("AppGimnasioMVC.Models.Rutina", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("Bloqueado")
                        .HasColumnType("bit");

                    b.Property<string>("Cardio")
                        .HasMaxLength(400)
                        .HasColumnType("nvarchar(400)");

                    b.Property<string>("Codigo")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Ejercicio1")
                        .IsRequired()
                        .HasMaxLength(400)
                        .HasColumnType("nvarchar(400)");

                    b.Property<string>("Ejercicio2")
                        .HasMaxLength(400)
                        .HasColumnType("nvarchar(400)");

                    b.Property<string>("Ejercicio3")
                        .HasMaxLength(400)
                        .HasColumnType("nvarchar(400)");

                    b.Property<string>("Ejercicio4")
                        .HasMaxLength(400)
                        .HasColumnType("nvarchar(400)");

                    b.Property<string>("Ejercicio5")
                        .HasMaxLength(400)
                        .HasColumnType("nvarchar(400)");

                    b.Property<string>("NombreRutina")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.ToTable("Rutina");
                });

            modelBuilder.Entity("AppGimnasioMVC.Models.Cliente", b =>
                {
                    b.HasBaseType("AppGimnasioMVC.Models.Persona");

                    b.Property<int>("IngresoGimnasioId")
                        .HasColumnType("int");

                    b.Property<int>("MensualidadId")
                        .HasColumnType("int");

                    b.Property<string>("Peso")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RutinaId")
                        .HasColumnType("int");

                    b.HasIndex("IngresoGimnasioId");

                    b.HasIndex("MensualidadId");

                    b.HasIndex("RutinaId");

                    b.HasDiscriminator().HasValue("Cliente");
                });

            modelBuilder.Entity("AppGimnasioMVC.Models.Entrenador", b =>
                {
                    b.HasBaseType("AppGimnasioMVC.Models.Persona");

                    b.Property<string>("NumeroCuenta")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasDiscriminator().HasValue("Entrenador");
                });

            modelBuilder.Entity("AppGimnasioMVC.Models.Cliente", b =>
                {
                    b.HasOne("AppGimnasioMVC.Models.IngresoGimnasio", "IngresoGimnasio")
                        .WithMany()
                        .HasForeignKey("IngresoGimnasioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AppGimnasioMVC.Models.Mensualidad", "Mensualidad")
                        .WithMany()
                        .HasForeignKey("MensualidadId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AppGimnasioMVC.Models.Rutina", "Rutina")
                        .WithMany()
                        .HasForeignKey("RutinaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("IngresoGimnasio");

                    b.Navigation("Mensualidad");

                    b.Navigation("Rutina");
                });
#pragma warning restore 612, 618
        }
    }
}