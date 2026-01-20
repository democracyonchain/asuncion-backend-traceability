namespace Asuncion.Backend.Traceability.Dtos
{
    public class StepDto
    {
        public string txHash { get; set; } = string.Empty;
        public string? timestamp { get; set; } // "YYYY-MM-DD HH:mm" (o ISO)
        public long? block { get; set; }       // opcional
        public object? metadata { get; set; }  // JSON libre (CIP o tu esquema)
        public string? result { get; set; }    // "APROBADO" | "OBSERVADO" (solo en control si quieres)
    }
}
