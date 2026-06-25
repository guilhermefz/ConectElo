namespace ConectElo.Application.Areas.AmigoSecreto.DTOs
{
    public class SorteioExecutadoDto
    {
        public Guid EventoId { get; set; }
        public DateTime DataExecucao { get; set; }
        public int TotalPares { get; set; }
        public List<Guid> ParticipantesIds { get; set; } = new();
    }
}
