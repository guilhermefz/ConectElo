using ConectElo.Domain.Areas.Base.Interfaces;
using ConectElo.Domain.Areas.Dinamicas.Entities;

namespace ConectElo.Domain.Areas.Eventos.InterfacesRepository
{
    public interface IItensListaDesejosRepository : IGenericRepository<ItensListaDesejos>
    {
        Task<ItensListaDesejos?> BuscarPorId(Guid id);
    }
}
