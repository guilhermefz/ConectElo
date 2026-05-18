using ConectElo.Application.Areas.Comunicacao.DTOs;

namespace ConectElo.Application.Areas.Comunicacao.InterfacesService
{
    public interface IMensagemService
    {
        Task<IEnumerable<MensagemDto>> ObterHistoricoAsync(Guid grupoId, Guid usuarioId);
        Task<MensagemDto> EnviarMensagemAsync(Guid grupoId, Guid usuarioId, string conteudo);
    }
}
