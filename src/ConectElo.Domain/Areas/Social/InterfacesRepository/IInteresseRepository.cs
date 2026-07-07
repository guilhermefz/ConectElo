using ConectElo.Domain.Areas.Base.Interfaces;
using ConectElo.Domain.Areas.Social.Entities;

namespace ConectElo.Domain.Areas.Social.InterfacesRepository
{
    public interface IInteresseRepository : IGenericRepository<Interesse>
    {
        Task<List<Interesse>> ListarTodos();
        Task<List<Interesse>> ListarPorIds(List<Guid> ids);
        Task<Usuario?> ObterUsuarioComInteresses(Guid usuarioId);
    }
}
