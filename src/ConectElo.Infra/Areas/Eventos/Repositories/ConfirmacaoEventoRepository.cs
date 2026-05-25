using ConectElo.Domain.Areas.Eventos.InterfacesRepository;
using ConectElo.Domain.Areas.Geral.Entities;
using ConectElo.Domain.Areas.Geral.Enuns;
using ConectElo.Infra.Areas.Base;
using ConectElo.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace ConectElo.Infra.Areas.Eventos.Repositories
{
    public class ConfirmacaoEventoRepository : RepositoryGeneric<ConfirmacaoEvento>, IConfirmacaoEventoRepository
    {
        private readonly AppDbContext _context;

        public ConfirmacaoEventoRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Dictionary<Guid, StatusConfirmacaoEventoEnum?>> BuscarParticipacoesPorEventos(List<Guid> eventoIds, Guid usuarioId)
        {
            var confirmacoes = await _context.ConfirmacaoEventos
                                        .Where(c => eventoIds.Contains(c.EventoId)
                                        && c.UsuarioId == usuarioId).ToListAsync();

            return eventoIds.ToDictionary(id => id,
                                          id => (StatusConfirmacaoEventoEnum?)confirmacoes.FirstOrDefault(c =>
                                          c.EventoId == id)?.Status);
        }

        public async Task<ConfirmacaoEvento?> BuscarPorEventoEUsuario(Guid eventoId, Guid usuarioId)
        {
            return await _context.ConfirmacaoEventos
                                    .FirstOrDefaultAsync(c => c.EventoId == eventoId
                                                        && c.UsuarioId == usuarioId);
        }

        public async Task<List<ConfirmacaoEvento>> ListarPorEvento(Guid eventoId)
            => await _context.ConfirmacaoEventos
                .Include(c => c.Usuario)
                .Where(c => c.EventoId == eventoId)
                .ToListAsync();
    }
}
