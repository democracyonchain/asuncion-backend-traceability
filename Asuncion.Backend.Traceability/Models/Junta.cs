namespace Asuncion.Backend.Traceability.Models
{
    public class Junta
    {
        public int Id { get; set; }

        public int Provincia_Id { get; set; }
        public int Canton_Id { get; set; }
        public int Parroquia_Id { get; set; }
        public int Zona_Id { get; set; }

        public int Junta_Numero { get; set; } // columna: junta
        public string? Sexo { get; set; }     // 'M' / 'F'
        public int? Electores { get; set; }
    }
}
