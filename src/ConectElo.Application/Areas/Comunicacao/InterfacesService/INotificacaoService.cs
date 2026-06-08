using ConectElo.Application.Areas.Comunicacao.DTOs;

namespace ConectElo.Application.Areas.Comunicacao.InterfacesService
{
    public interface INotificacaoService
    {
        Task<List<ExibirNotificacaoDto>> ListarPorUsuario(Guid usuarioId);
        Task MarcarComoLida(Guid notificacaoId, Guid usuarioId);
        Task<List<ExibirNotificacaoDto>> CriarNotificacoesNovoEvento(Guid eventoId, Guid grupoId, string nomeEvento, string nomeCriador, Guid criadorId);
    }
}
