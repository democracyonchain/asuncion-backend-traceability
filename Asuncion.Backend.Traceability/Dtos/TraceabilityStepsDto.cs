namespace Asuncion.Backend.Traceability.Dtos
{
    public class TraceabilityStepsDto
    {
        public StepDto escaneo { get; set; } = new();
        public StepDto digitacion { get; set; } = new();
        public StepDto control { get; set; } = new();
    }
}
