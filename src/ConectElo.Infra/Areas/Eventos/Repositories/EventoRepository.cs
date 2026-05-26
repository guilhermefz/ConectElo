using ConectElo.Domain.Areas.Dinamicas.Entities;
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

        public async Task<List<Evento>> ListarPorUsuario(Guid usuarioId)
        {
            return await _context.Set<Evento>()
                .Where(e => e.DataDelecao == null &&
                    (e.Criador == usuarioId ||
                     e.Grupo!.Membros.Any(m => m.UsuarioId == usuarioId && m.DataSaida == null)))
                .Include(e => e.CriadorEvento)
                .OrderByDescending(e => e.DataCriacao)
                .ToListAsync();
        }

        public override async Task<Evento?> SelecionarPorId(Guid id)
        {
            var aniversario = await _context.Set<AniversarioEvento>()
                .Include(a => a.CriadorEvento)
                .Include(a => a.ListaDesejos!)
                    .ThenInclude(l => l.Itens)
                        .ThenInclude(i => i.ReservadoPor)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (aniversario is not null)
                return aniversario;

            return await _context.Set<Evento>()
                .Include(e => e.CriadorEvento)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<AniversarioEvento?> BuscarAniversarioPorListaDesejosId(Guid listaId)
        {
            return await _context.Set<AniversarioEvento>()
                .FirstOrDefaultAsync(a => a.ListaDesejosId == listaId);
        }
    }
}
