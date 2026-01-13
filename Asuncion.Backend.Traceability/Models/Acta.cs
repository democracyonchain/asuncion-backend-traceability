namespace Asuncion.Backend.Traceability.Models
{
    public class Acta
    {
        public int Id { get; set; }
        public int? Dignidad_Id { get; set; }
        public int? Junta_Id { get; set; }
        public int? Seguridad { get; set; }
        public int? Estado { get; set; }

        public int? UsuarioEscaneo { get; set; }
        public DateTime? FechaEscaneo { get; set; }

        public int? UsuarioDigitacion { get; set; }
        public DateTime? FechaDigitacion { get; set; }

        public int? UsuarioControl { get; set; }
        public DateTime? FechaControl { get; set; }

        public int? Peticion { get; set; }

        public int? SufragantesICR { get; set; }
        public int? SufragantesDigitacion { get; set; }
        public int? SufragantesControl { get; set; }
        public int? Sufragantes { get; set; }

        public int? BlancosICR { get; set; }
        public int? BlancosDigitacion { get; set; }
        public int? BlancosControl { get; set; }
        public int? Blancos { get; set; }

        public int? NulosICR { get; set; }
        public int? NulosDigitacion { get; set; }
        public int? Nulos { get; set; }

        public bool Bloqueo { get; set; }

        public string? TxICR { get; set; }
        public string? TxDigitacion { get; set; }
        public string? TxControl { get; set; }
    }
}
