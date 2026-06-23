using ConectElo.Domain.Areas.Base;
using ConectElo.Domain.Areas.Social.Entities;

namespace ConectElo.Domain.Areas.Dinamicas.Entities
{
    public class ResultadoSorteio : EntityBase
    {
        public Guid EventoId { get; set; }
        public virtual AmigoSecretoEvento Evento { get; set; }

        public Guid PresenteadorId { get; set; }
        public virtual Usuario Presenteador { get; set; }

        public Guid RecebedorId { get; set; }
        public virtual Usuario Recebedor { get; set; }

        public DateTime DataSorteio { get; set; }

        public virtual ICollection<MensagemAnonima> Mensagens { get; set; } = new List<MensagemAnonima>();
    }
}
