using ConectElo.Domain.Areas.Dinamicas.Entities;
using ConectElo.Domain.Areas.Eventos.InterfacesRepository;
using ConectElo.Infra.Areas.Base;
using ConectElo.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace ConectElo.Infra.Areas.Eventos.Repositories
{
    public class ResultadoSorteioRepository : RepositoryGeneric<ResultadoSorteio>, IResultadoSorteioRepository
    {
        private readonly AppDbContext _context;

        public ResultadoSorteioRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<ResultadoSorteio>> BuscarPorEvento(Guid eventoId)
        {
            return await _context.ResultadoSorteios
                .Include(r => r.Presenteador)
                .Include(r => r.Recebedor)
                .Where(r => r.EventoId == eventoId)
                .ToListAsync();
        }

        public async Task<ResultadoSorteio?> BuscarComoPresenteador(Guid eventoId, Guid usuarioId)
        {
            return await _context.ResultadoSorteios
                .Include(r => r.Recebedor)
                .FirstOrDefaultAsync(r =>
                    r.EventoId == eventoId &&
                    r.PresenteadorId == usuarioId);
        }

        public async Task<ResultadoSorteio?> BuscarComoRecebedor(Guid eventoId, Guid usuarioId)
        {
            return await _context.ResultadoSorteios
                .FirstOrDefaultAsync(r =>
                    r.EventoId == eventoId &&
                    r.RecebedorId == usuarioId);
        }

        public async Task<bool> EventoJaFoiSorteado(Guid eventoId)
        {
            return await _context.ResultadoSorteios.AnyAsync(r => r.EventoId == eventoId);
        }
    }
}
