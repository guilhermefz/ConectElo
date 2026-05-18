using ConectElo.Domain.Areas.Base.Interfaces;
using ConectElo.Domain.Areas.Comunicacao.Entities;

namespace ConectElo.Domain.Areas.Comunicacao.InterfacesRepository
{
    public interface IMensagemRepository : IGenericRepository<Mensagem>
    {
        Task<IEnumerable<Mensagem>> ObterMensagensDoGrupoAsync(Guid grupoId, int quantidade = 50);
    }
}
