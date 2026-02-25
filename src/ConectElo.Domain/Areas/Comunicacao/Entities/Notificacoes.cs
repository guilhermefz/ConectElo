using ConectElo.Domain.Areas.Base;
using ConectElo.Domain.Areas.Comunicacao.Enuns;

namespace ConectElo.Domain.Areas.Comunicacao.Entities
{
    public class Notificacoes : EntityBase
    {
        public string Conteudo { get; set; }
        public string LinkUrl { get; set; }
        public bool NotificacaoLida { get; set; }
        public DateTime DataEnvio { get; set; }
        public TipoNotificacaoEnum TipoNotificacao { get; set; }
        public Guid UsuarioId { get; set; }
    }
}
