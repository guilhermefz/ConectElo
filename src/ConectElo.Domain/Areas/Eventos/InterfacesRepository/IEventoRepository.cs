using ConectElo.Domain.Areas.Base.Interfaces;
using ConectElo.Domain.Areas.Eventos.Entities;

namespace ConectElo.Domain.Areas.Eventos.InterfacesRepository
{
    public interface IEventoRepository : IGenericRepository<Evento>
    {
        Task<List<Evento>> ListarPorGrupo(Guid grupoId);

        Task<List<Evento>> ListarPorUsuario(Guid usuarioId);
    }
}
