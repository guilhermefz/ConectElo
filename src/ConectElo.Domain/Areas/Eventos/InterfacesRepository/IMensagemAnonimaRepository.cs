using ConectElo.Domain.Areas.Base.Interfaces;
using ConectElo.Domain.Areas.Dinamicas.Entities;

namespace ConectElo.Domain.Areas.Eventos.InterfacesRepository
{
    public interface IMensagemAnonimaRepository : IGenericRepository<MensagemAnonima>
    {
        Task<List<MensagemAnonima>> ListarPorResultado(Guid resultadoSorteioId);
    }
}
