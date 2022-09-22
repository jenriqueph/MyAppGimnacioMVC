using System.ComponentModel;

namespace AppGimnasioMVC.Models
{
    public enum TipoDocId
    {
        [Description("Registro Civil")]
        RegistroCivil = 3,
        [Description("Tarjeta Identidad")]
        TarjetaIdentidad = 2,
        [Description("Cédula Ciudadanía")]
        CedulaCiudadania = 1,
        [Description("Tarjeta Extranjería")]
        TarjetaExtranjeria = 4,
        [Description("Cédula Extranjería")]
        CedulaExtranjeria = 5,
        NIT = 6,
        Pasaporte = 7,
        [Description("Tipo Documento Extranjero")]
        TipoDocumentoExtranjero = 8
    }
}
