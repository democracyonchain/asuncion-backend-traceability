namespace Asuncion.Backend.Traceability.Dtos
{
    public class ResultadosDto
    {
        public string? estadoFinal { get; set; } // APROBADO / OBSERVADO / etc.
        public string? fuente { get; set; }      // control | digitacion | oficial
        public List<ResultadoCandidatoDto>? candidatos { get; set; }
    }

    public class ResultadoCandidatoDto
    {
        public int candidatoId { get; set; }
        public string nombre { get; set; } = "";
        public int votos { get; set; }
    }
}
