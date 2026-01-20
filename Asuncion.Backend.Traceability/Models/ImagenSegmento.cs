namespace Asuncion.Backend.Traceability.Models
{
    public class ImagenSegmento
    {
        public int Junta_Id { get; set; }
        public int Dignidad_Id { get; set; }
        public int Candidato_Id { get; set; }

        public string? Nombre { get; set; }
        public string? Hash { get; set; }
        public string? PathIpfs { get; set; }
        public DateTime? Fecha { get; set; }

        public byte[]? Imagen { get; set; }
    }
}
