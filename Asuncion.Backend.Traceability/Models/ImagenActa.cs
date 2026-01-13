namespace Asuncion.Backend.Traceability.Models
{
    public class ImagenActa
    {
        public int Acta_Id { get; set; }
        public string? Nombre { get; set; }
        public short? Pagina { get; set; }

        public string? Hash { get; set; }
        public string? PathIpfs { get; set; }
        public DateTime? Fecha { get; set; }

        // cuidado: puede ser grande; en trazabilidad normalmente NO necesitas traerlo
        public byte[]? Imagen { get; set; }
    }
}
