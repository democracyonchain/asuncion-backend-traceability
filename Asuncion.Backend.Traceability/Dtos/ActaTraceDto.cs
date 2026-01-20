namespace Asuncion.Backend.Traceability.Dtos
{
    public class ActaTraceDto
    {
        public int actaId { get; set; }
        public string codigo { get; set; } = "";

        public ActaScopeDto scope { get; set; } = new();

        // ✅ NUEVO: arreglo de imágenes (metadata + CID)
        public List<ImagenActaDto> imagenes { get; set; } = new();

        public TraceabilityStepsDto steps { get; set; } = new();

        public ResultadosDto? resultados { get; set; }

        public string? lastUpdated { get; set; }
    }
}
