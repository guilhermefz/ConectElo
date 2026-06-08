using ConectElo.Domain.Areas.Comunicacao.Enuns;

namespace ConectElo.Application.Areas.Comunicacao.DTOs
{
    public class ExibirNotificacaoDto
    {
        public Guid Id { get; set; }
        public Guid UsuarioId { get; set; }
        public string Conteudo { get; set; }
        public string LinkUrl { get; set; }
        public bool NotificacaoLida { get; set; }
        public DateTime DataEnvio { get; set; }
        public TipoNotificacaoEnum TipoNotificacao { get; set; }
    }
}
