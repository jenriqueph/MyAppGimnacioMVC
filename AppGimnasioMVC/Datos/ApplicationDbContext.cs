using AppGimnasioMVC.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace AppGimnasioMVC.Datos
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        //usar los modelos
        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<Entrenador> Entrenador { get; set; }
        public DbSet<IngresoGimnasio> IngresoGimnasio { get; set; }
        public DbSet<Mensualidad> Mensualidad { get; set; }
        public DbSet<Persona> Persona { get; set; }
        public DbSet<Rutina> Rutina { get; set; }
    }
}
