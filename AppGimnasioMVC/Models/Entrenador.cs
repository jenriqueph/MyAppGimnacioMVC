using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace AppGimnasioMVC.Models
{
    public class Entrenador : Persona
    {
        [Required(ErrorMessage = "El campo número de cuenta es obligatorio.")]
        [StringLength(30, ErrorMessage = "{0} el número de identificación debe tener entre {2} y {1}.", MinimumLength = 4)]
        [Display(Name = "Número de Cuenta")]
        public string NumeroCuenta { get; set; }
    }
}
