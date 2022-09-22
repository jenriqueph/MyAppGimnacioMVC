namespace AppGimnasioMVC.Models
{
    public class Cliente : Persona
    {
        public string? Peso { get; set; }
        public IngresoGimnasio? IngresoGimnasio { get; set; }
        public Mensualidad? Mensualidad { get; set; }
        public Rutina? Rutina { get; set; }

        /*
            [Required]
            public int MensualidadID { get; set; }
            public Mensualidad Mensualidad { get; set; }
         */

    }
}
