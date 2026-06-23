using ConectElo.Domain.Areas.Base;
using ConectElo.Domain.Areas.Dinamicas.Enuns;

namespace ConectElo.Domain.Areas.Dinamicas.Entities
{
    public class MensagemAnonima : EntityBase
    {
        public Guid ResultadoSorteioId { get; set; }
        public virtual ResultadoSorteio ResultadoSorteio { get; set; }

        public string Conteudo { get; set; }
        public DateTime HorarioEnvio { get; set; }

        public ParticipanteTipoEnum ParticipanteTipo { get; set; }
    }
}
