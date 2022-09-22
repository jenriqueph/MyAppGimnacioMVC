using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace AppGimnasioMVC.Models
{
    public class Persona
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo número de identificación es obligatorio.")]
        [StringLength(50, ErrorMessage = "{0} el número de identificación debe tener entre {2} y {1}.", MinimumLength = 4)]
        [Display(Name = "Número de Identificación")]
        public string NumeroIdentificacion { get; set; }
        [Required(ErrorMessage = "El campo tipo de documento de identificación es obligatorio.")]
        [Display(Name = "Tipo Documento Identificación")]
        public TipoDocId TipoDocIdentificacion { get; set; }
        [Required(ErrorMessage = "El campo nombres es obligatorio.")]
        [StringLength(100, ErrorMessage = "{0} el campo nombres debe tener entre {2} y {1}.", MinimumLength = 2)]
        public string Nombres { get; set; }
        [Required(ErrorMessage = "El campo apellidos es obligatorio.")]
        [StringLength(100, ErrorMessage = "{0} el campo apellidos debe tener entre {2} y {1}.", MinimumLength = 2)]
        public string Apellidos { get; set; }
        [StringLength(200, ErrorMessage = "{0} el campo dirección debe tener entre {2} y {1}.", MinimumLength = 4)]
        [Display(Name = "Dirección")]
        public string? Direccion { get; set; }
        [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
        [EmailAddress]
        [Display(Name = "Correo Electrónico")]
        public string Email { get; set; }
        [Required(ErrorMessage = "La contraseña es obligatorio.")]
        [StringLength(100, ErrorMessage = "{0} el campo contraseña debe tener entre {2} y {1}.", MinimumLength = 2)]
        [Display(Name = "Contraseña")]
        public string Contrasenia { get; set; }
        [StringLength(10, ErrorMessage = "{0} el campo celular debe tener entre {2} y {1}.", MinimumLength = 10)]
        public string? Celular { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Fecha de Nacimiento")]
        public DateTime? FechaNacimiento { get; set; }
        public Genero Genero { get; set; }
        public Rol Rol { get; set; }
        [Required(ErrorMessage = "El campo bloqueado es obligatorio.")]
        public Boolean Bloqueado { get; set; }
    }
}
