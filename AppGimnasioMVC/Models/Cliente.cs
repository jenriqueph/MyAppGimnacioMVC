namespace AppGimnasioMVC.Models
{
    public class Cliente : Persona
    {
        public string? Peso { get; set; }
        /*
        public int? IngresoGimnasioId { get; set; }
        
        public IngresoGimnasio? IngresoGimnasio { get; set; }
        
        public int? MensualidadId { get; set; }
        public Mensualidad? Mensualidad { get; set; }
        */
        public int? RutinaId { get; set; }
        //public Rutina? Rutina { get; set; }

        public Rutina? Rutina { get; set; }
        /*
            [Required]
            public int MensualidadId { get; set; }
            public Mensualidad Mensualidad { get; set; }
         */

    }
}
