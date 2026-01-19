namespace Asuncion.Backend.Traceability.Dtos
{
    public class IpfsDto
    {
        public bool hasImage { get; set; }
        public string? cid { get; set; }        // pathipfs (Qm...)
        public string? nombre { get; set; }     // nombre
        public short? pagina { get; set; }      // pagina
        public string? source { get; set; }     // "db"
        public DateTime? fecha { get; set; }  // opcional
    }
}
