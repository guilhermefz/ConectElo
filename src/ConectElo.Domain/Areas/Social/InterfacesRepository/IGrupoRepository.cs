using ConectElo.Domain.Areas.Base.Interfaces;
using ConectElo.Domain.Areas.Social.Entities;

namespace ConectElo.Domain.Areas.Social.InterfacesRepository
{
    public interface IGrupoRepository : IGenericRepository<Grupo>
    {
        Task<Grupo?> ObterGrupoComInclude(Guid id);
        Task<IEnumerable<Grupo>> BuscarPorUsuario(Guid usuarioId);
        Task<Grupo?> BuscarPorCodigoConvite(string codigo);
    }
}
