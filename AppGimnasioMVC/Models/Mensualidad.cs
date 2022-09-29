using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace AppGimnasioMVC.Models
{
    public class Mensualidad
    {
        /*
        public Mensualidad()
        {
            Clientes = new HashSet<Cliente>();
        }

        public virtual ICollection<Cliente> Clientes { get; set; }
        */
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo valor de la mensualidad es obligatorio.")]
        [Range(50, 99999999, ErrorMessage = "El valor para {0} debe ser entre {1} y {2}.")]
        [Display(Name = "Valor de Mensualidad")]
        public double ValorMensualidad { get; set; }
        [Required(ErrorMessage = "La fecha de registro es obligatoria.")]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de registro")]
        public DateTime Fecha { get; set; }
        [Required(ErrorMessage = "La fecha de inicio es obligatoria.")]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de Inicio")]
        public DateTime FechaInicio { get; set; }
        [Required(ErrorMessage = "La fecha final es obligatoria.")]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha Final")]
        public DateTime FechaFin { get; set; }
        [Required(ErrorMessage = "El campo bloqueado es obligatorio.")]
        public Boolean Bloqueado { get; set; }

        [Required(ErrorMessage = "El campo cliente es obligatorio.")]
        [Display(Name = "Cliente")]
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }
    }
}
