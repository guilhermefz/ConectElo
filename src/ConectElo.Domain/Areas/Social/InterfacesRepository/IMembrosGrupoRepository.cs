using ConectElo.Domain.Areas.Base.Interfaces;
using ConectElo.Domain.Areas.Social.Entities;

namespace ConectElo.Domain.Areas.Social.InterfacesRepository
{
    public interface IMembrosGrupoRepository : IGenericRepository<MembrosGrupo>
    {
        Task<bool> VerificarMembroASync(Guid grupoId, Guid usuarioId);
        Task<MembrosGrupo?> BuscarMembroPorGrupoEUsuario(Guid grupoId, Guid usuarioId);
    }
}
