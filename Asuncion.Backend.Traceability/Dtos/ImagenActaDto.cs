namespace Asuncion.Backend.Traceability.Dtos
{
    public class ImagenActaDto
    {
        public int? acta_id { get; set; }
        public string? nombre { get; set; }
        public short? pagina { get; set; }
        public string? hash { get; set; }
        public string? pathipfs { get; set; }
        public DateTime? fecha { get; set; }
    }
}
