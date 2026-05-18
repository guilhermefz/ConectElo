using ConectElo.Domain.Areas.Eventos.Entities;
using ConectElo.Domain.Areas.Eventos.InterfacesRepository;
using ConectElo.Infra.Areas.Base;
using ConectElo.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace ConectElo.Infra.Areas.Eventos.Repositories
{
    public class EventoRepository : RepositoryGeneric<Evento>, IEventoRepository
    {
        private readonly AppDbContext _context;

        public EventoRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Evento>> ListarPorGrupo(Guid grupoId)
        {
            return await _context.Set<Evento>()
                .Where(e => e.GrupoId == grupoId && e.DataDelecao == null)
                .OrderByDescending(e => e.DataCriacao)
                .ToListAsync();
        }
    }
}
