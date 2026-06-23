using ConectElo.Domain.Areas.Base.Interfaces;
using ConectElo.Domain.Areas.Dinamicas.Entities;

namespace ConectElo.Domain.Areas.Eventos.InterfacesRepository
{
    public interface IResultadoSorteioRepository : IGenericRepository<ResultadoSorteio>
    {
        Task<List<ResultadoSorteio>> BuscarPorEvento(Guid eventoId);
        Task<ResultadoSorteio?> BuscarComoPresenteador(Guid eventoId, Guid usuarioId);
        Task<ResultadoSorteio?> BuscarComoRecebedor(Guid eventoId, Guid usuarioId);
        Task<bool> EventoJaFoiSorteado(Guid eventoId);
    }
}
