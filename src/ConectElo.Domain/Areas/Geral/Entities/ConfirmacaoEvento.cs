using ConectElo.Domain.Areas.Base;
using ConectElo.Domain.Areas.Eventos.Entities;
using ConectElo.Domain.Areas.Social.Entities;

namespace ConectElo.Domain.Areas.Geral.Entities
{
    public class ConfirmacaoEvento : EntityBase
    {
        public StatusConfirmacaoEventoEnum Status {  get; set; }
        public DateTime DataAtualizacao { get; set; }
        public Guid EventoId { get; set; }
        public Evento? Evento { get; set; }
        public Guid UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }
    }
}
