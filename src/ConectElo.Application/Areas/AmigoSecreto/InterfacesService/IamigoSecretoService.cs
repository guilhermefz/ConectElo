using ConectElo.Application.Areas.AmigoSecreto.DTOs;

namespace ConectElo.Application.Areas.AmigoSecreto.InterfacesService
{
    public interface IAmigoSecretoService
    {
        Task<string> AgendarSorteio( AgendarSorteioDto dto, Guid criadorId);
        Task<SorteioExecutadoDto> ExecutarSorteio( Guid eventoId);
        Task<SorteioExecutadoDto> SortearAgora( Guid eventoId, Guid criadorId);
        Task<string> AlterarDataSorteio( Guid eventoId, DateTime novaData, Guid criadorId);
        Task<MeuResultadoDto> BuscarMeuResultado( Guid eventoId, Guid usuarioId);
        Task<List<MensagemAnonimaDto>> BuscarHistorico( Guid resultadoSorteioId, Guid usuarioId);
        Task<MensagemAnonimaDto> EnviarMensagem( Guid resultadoSorteioId, Guid usuarioId, string conteudo);
    }
}
