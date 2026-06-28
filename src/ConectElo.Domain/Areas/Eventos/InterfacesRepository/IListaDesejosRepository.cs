using ConectElo.Domain.Areas.Base.Interfaces;
using ConectElo.Domain.Areas.Dinamicas.Entities;

namespace ConectElo.Domain.Areas.Eventos.InterfacesRepository
{
    public interface IListaDesejosRepository : IGenericRepository<ListaDesejos>
    {
        Task<ListaDesejos?> BuscarPorEventoEUsuario(Guid eventoId, Guid usuarioId);
    }
}
