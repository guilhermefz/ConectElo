using ConectElo.Domain.Areas.Base.Interfaces;
using ConectElo.Domain.Areas.Social.Entities;

namespace ConectElo.Domain.Areas.Social.InterfacesRepository
{
    public interface IPostagemRepository : IGenericRepository<Postagens>
    {
        Task<IEnumerable<Postagens>> ObterFeedDoUsuario(Guid usuarioId, int pagina, int tamanhoPagina);
    }
}
