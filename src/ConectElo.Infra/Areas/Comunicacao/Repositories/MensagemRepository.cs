using ConectElo.Domain.Areas.Comunicacao.Entities;
using ConectElo.Domain.Areas.Comunicacao.InterfacesRepository;
using ConectElo.Infra.Areas.Base;
using ConectElo.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace ConectElo.Infra.Areas.Comunicacao.Repositories
{
    public class MensagemRepository : RepositoryGeneric<Mensagem>, IMensagemRepository
    {
        public MensagemRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Mensagem>> ObterMensagensDoGrupoAsync(Guid grupoId, int quantidade = 50)
        {
            return await Consultar()
                .Where(mensagem => mensagem.GrupoId == grupoId)
                .Include(mensagem => mensagem.Autor)
                .OrderByDescending(mensagem => mensagem.HorarioEnvio)
                .Take(quantidade)
                .ToListAsync();
        }
    }
}
