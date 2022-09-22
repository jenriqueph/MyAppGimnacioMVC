using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace AppGimnasioMVC.Models
{
    public class IngresoGimnasio
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "La fecha de ingreso es obligatoria.")]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de Ingreso")]
        public DateTime FechaIngreso { get; set; }
        [Required(ErrorMessage = "El campo bloqueado es obligatorio.")]
        public Boolean Bloqueado { get; set; }
    }
}
