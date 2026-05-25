using ConectElo.Domain.Areas.Base.Interfaces;
using ConectElo.Domain.Areas.Geral.Entities;
using ConectElo.Domain.Areas.Geral.Enuns;

namespace ConectElo.Domain.Areas.Eventos.InterfacesRepository
{
    public interface IConfirmacaoEventoRepository : IGenericRepository<ConfirmacaoEvento>
    {
        Task<ConfirmacaoEvento?> BuscarPorEventoEUsuario(Guid eventoId, Guid usuarioId);
        Task<Dictionary<Guid, StatusConfirmacaoEventoEnum?>> BuscarParticipacoesPorEventos(List<Guid> eventoIds, Guid usuarioId);
        Task<List<ConfirmacaoEvento>> ListarPorEvento(Guid eventoId);
    }
}
