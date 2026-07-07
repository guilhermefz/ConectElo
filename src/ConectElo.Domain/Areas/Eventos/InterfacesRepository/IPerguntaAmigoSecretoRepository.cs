using ConectElo.Domain.Areas.Base.Interfaces;
using ConectElo.Domain.Areas.Dinamicas.Entities;

namespace ConectElo.Domain.Areas.Eventos.InterfacesRepository
{
    public interface IPerguntaAmigoSecretoRepository : IGenericRepository<PerguntaAmigoSecreto>
    {
        Task<List<PerguntaAmigoSecreto>> ListarAtivasPorResultado(Guid resultadoSorteioId);
        Task<List<PerguntaAmigoSecreto>> ListarRecebidasPorEvento(Guid eventoId, Guid recebedorId);
        Task<PerguntaAmigoSecreto?> BuscarCompletaPorId(Guid id);
        Task<int> ContarAtivasPorResultado(Guid resultadoSorteioId);
        Task<bool> ExisteAtivaComPergunta(Guid resultadoSorteioId, Guid perguntaQuizId);
    }
}
