using ConectElo.Application.Areas.AmigoSecreto.DTOs;
using ConectElo.Application.Areas.Social.DTOs.EventosDTO;

namespace ConectElo.Application.Areas.AmigoSecreto.InterfacesService
{
    public interface IAmigoSecretoService
    {
        Task<string> AgendarSorteio( AgendarSorteioDto dto, Guid criadorId);
        Task<SorteioExecutadoDto> Sortear( Guid eventoId, Guid criadorId);
        Task<SorteioExecutadoDto> ExecutarSorteio( Guid eventoId);
        Task<SorteioExecutadoDto> SortearAgora( Guid eventoId, Guid criadorId);
        Task<string> AlterarDataSorteio( Guid eventoId, DateTime novaData, Guid criadorId);
        Task<MeuResultadoDto> BuscarMeuResultado( Guid eventoId, Guid usuarioId);
        Task<List<MensagemAnonimaDto>> BuscarHistorico( Guid resultadoSorteioId, Guid usuarioId);
        Task<MensagemAnonimaDto> EnviarMensagem( Guid resultadoSorteioId, Guid usuarioId, string conteudo);
        Task<ExibirListaDesejosDto> BuscarMinhaLista( Guid eventoId, Guid usuarioId);
        Task<ExibirItemListaDesejosDto> AdicionarItemMinhaLista( Guid eventoId, Guid usuarioId, CriarItemListaDesejosDto dto);
        Task RemoverItemMinhaLista( Guid itemId, Guid usuarioId);

        // Detalhe + quiz
        Task<AmigoSecretoDetalheDto> BuscarDetalhe( Guid eventoId, Guid usuarioId);
        Task<List<PerguntaCatalogoDto>> ListarCatalogoQuiz();
        Task<PerguntarQuizResultadoDto> PerguntarQuiz( Guid eventoId, Guid usuarioId, Guid perguntaQuizId);
        Task<PerguntaAtivaDto> TrocarPerguntaQuiz( Guid perguntaAmigoSecretoId, Guid usuarioId, Guid novaPerguntaQuizId);
        Task<PerguntaRecebidaDto> ResponderQuiz( Guid perguntaAmigoSecretoId, Guid usuarioId, Guid opcaoId);
        Task<List<PerguntaRecebidaDto>> ListarPerguntasRecebidas( Guid eventoId, Guid usuarioId);
    }
}
