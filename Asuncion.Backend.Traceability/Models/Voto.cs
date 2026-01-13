namespace Asuncion.Backend.Traceability.Models
{
    public class Voto
    {
        public int Acta_Id { get; set; }
        public int Candidato_Id { get; set; }

        public int? VotosICR { get; set; }
        public int? VotosDigitacion { get; set; }
        public int? VotosControl { get; set; }
        public int? Votos { get; set; }

        public int? UsuarioICR { get; set; }
        public int? UsuarioDigitalizacion { get; set; }
        public int? UsuarioControl { get; set; }

        public string? Cifrado { get; set; }
        public string? Hash { get; set; }
    }
}
