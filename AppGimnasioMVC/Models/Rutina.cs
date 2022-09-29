using System.ComponentModel.DataAnnotations;

namespace AppGimnasioMVC.Models
{
    public class Rutina
    {
        /*
        public Rutina()
        {
            Clientes = new HashSet<Cliente>();
        }

        public virtual ICollection<Cliente> Clientes { get; set; }
        */

        [Key]
        public int Id { get; set; }
        
        [Required(ErrorMessage = "El campo código es obligatorio.")]
        [StringLength(40, ErrorMessage = "{0} El código de la rutina debe tener entre {2} y {1}.", MinimumLength = 4)]
        [Display(Name = "Código")]
        public string Codigo { get; set; }
        
        [Required(ErrorMessage = "El campo descripción es obligatorio.")]
        [StringLength(100, ErrorMessage = "{0} El nombre de la rutina debe tener entre {2} y {1}.", MinimumLength = 1)]
        [Display(Name = "Nombre Rutina")]
        public string NombreRutina { get; set; }

        [Required(ErrorMessage = "El campo de rutina ejercicio 1 es obligatorio.")]
        [StringLength(400, ErrorMessage = "{0} El contenido del ejercicio debe tener entre {2} y {1}.", MinimumLength = 1)]
        [Display(Name = "Ejercicio 1")]
        public string? Ejercicio1 { get; set; }
        
        [StringLength(400, ErrorMessage = "{0} El contenido del ejercicio debe tener entre {2} y {1}.", MinimumLength = 1)]
        [Display(Name = "Ejercicio 2")]
        public string? Ejercicio2 { get; set; }
        
        [StringLength(400, ErrorMessage = "{0} El contenido del ejercicio debe tener entre {2} y {1}.", MinimumLength = 1)]
        [Display(Name = "Ejercicio 3")]
        public string? Ejercicio3 { get; set; }
        
        [StringLength(400, ErrorMessage = "{0} El contenido del ejercicio debe tener entre {2} y {1}.", MinimumLength = 1)]
        [Display(Name = "Ejercicio 4")]
        public string? Ejercicio4 { get; set; }
        
        [StringLength(400, ErrorMessage = "{0} El contenido del ejercicio debe tener entre {2} y {1}.", MinimumLength = 1)]
        [Display(Name = "Ejercicio 5")]
        public string? Ejercicio5 { get; set; }
        
        [StringLength(400, ErrorMessage = "{0} El contenido del cardio debe tener entre {2} y {1}.", MinimumLength = 1)]
        [Display(Name = "Cardio")]
        public string? Cardio { get; set; }
        
        [Required(ErrorMessage = "El campo bloqueado es obligatorio.")]
        public Boolean Bloqueado { get; set; }
    }
}
