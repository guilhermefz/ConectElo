using ConectElo.Domain.Areas.Base.Interfaces;
using ConectElo.Domain.Areas.Dinamicas.Entities;

namespace ConectElo.Domain.Areas.Eventos.InterfacesRepository
{
    public interface IPerguntaQuizRepository : IGenericRepository<PerguntaQuiz>
    {
        Task<List<PerguntaQuiz>> ListarAtivasComOpcoes();
        Task<PerguntaQuiz?> BuscarComOpcoes(Guid id);
    }
}
