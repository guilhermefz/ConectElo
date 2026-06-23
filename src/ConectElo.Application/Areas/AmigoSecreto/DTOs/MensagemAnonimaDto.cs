using ConectElo.Domain.Areas.Dinamicas.Enuns;

namespace ConectElo.Application.Areas.AmigoSecreto.DTOs
{
    public class MensagemAnonimaDto
    {
        public Guid Id { get; set; }
        public string Conteudo { get; set; }
        public DateTime HorarioEnvio { get; set; }
        public ParticipanteTipoEnum ParticipanteTipo { get; set; }
    }
}
