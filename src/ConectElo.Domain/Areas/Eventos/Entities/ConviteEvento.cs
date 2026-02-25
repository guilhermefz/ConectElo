using ConectElo.Domain.Areas.Base;
using ConectElo.Domain.Areas.Eventos.Enuns;
using ConectElo.Domain.Areas.Social.Entities;

namespace ConectElo.Domain.Areas.Eventos.Entities
{
    public class ConviteEvento : EntityBase
    {
        public string TokenConvite { get; set; }
        public StatusConviteEventoEnum Status { get; set;}
        public DateTime DataEnvio { get; set; }
        public Guid UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }
        public Guid EventoId { get; set; }
        public Evento? Evento { get; set; }
    }
}
