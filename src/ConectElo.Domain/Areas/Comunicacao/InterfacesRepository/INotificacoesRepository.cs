using ConectElo.Domain.Areas.Base.Interfaces;
using ConectElo.Domain.Areas.Comunicacao.Entities;

namespace ConectElo.Domain.Areas.Comunicacao.InterfacesRepository
{
    public interface INotificacoesRepository : IGenericRepository<Notificacoes>
    {
        Task<List<Notificacoes>> ListarPorUsuario(Guid usuarioId);
    }
}
