using ConectElo.Domain.Areas.Dinamicas.Entities;
using ConectElo.Domain.Areas.Dinamicas.Enuns;
using ConectElo.Domain.Areas.Eventos.InterfacesRepository;
using ConectElo.Infra.Areas.Base;
using ConectElo.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace ConectElo.Infra.Areas.Eventos.Repositories
{
    public class PerguntaAmigoSecretoRepository : RepositoryGeneric<PerguntaAmigoSecreto>, IPerguntaAmigoSecretoRepository
    {
        public PerguntaAmigoSecretoRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<List<PerguntaAmigoSecreto>> ListarAtivasPorResultado(Guid resultadoSorteioId)
        {
            return await _context.PerguntasAmigoSecreto
                .AsNoTracking()
                .Include(p => p.PerguntaQuiz)
                .Include(p => p.OpcaoResposta)
                .Where(p => p.ResultadoSorteioId == resultadoSorteioId && p.Status == StatusPerguntaEnum.Ativa)
                .OrderBy(p => p.PerguntadaEm)
                .ToListAsync();
        }

        public async Task<List<PerguntaAmigoSecreto>> ListarRecebidasPorEvento(Guid eventoId, Guid recebedorId)
        {
            return await _context.PerguntasAmigoSecreto
                .AsNoTracking()
                .Include(p => p.PerguntaQuiz)
                    .ThenInclude(q => q.Opcoes.OrderBy(o => o.Ordem))
                .Include(p => p.OpcaoResposta)
                .Include(p => p.ResultadoSorteio)
                .Where(p => p.Status == StatusPerguntaEnum.Ativa
                    && p.ResultadoSorteio.EventoId == eventoId
                    && p.ResultadoSorteio.RecebedorId == recebedorId)
                .OrderBy(p => p.PerguntadaEm)
                .ToListAsync();
        }

        public async Task<PerguntaAmigoSecreto?> BuscarCompletaPorId(Guid id)
        {
            return await _context.PerguntasAmigoSecreto
                .Include(p => p.ResultadoSorteio)
                .Include(p => p.PerguntaQuiz)
                    .ThenInclude(q => q.Opcoes.OrderBy(o => o.Ordem))
                .Include(p => p.OpcaoResposta)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<int> ContarAtivasPorResultado(Guid resultadoSorteioId)
        {
            return await _context.PerguntasAmigoSecreto
                .CountAsync(p => p.ResultadoSorteioId == resultadoSorteioId && p.Status == StatusPerguntaEnum.Ativa);
        }

        public async Task<bool> ExisteAtivaComPergunta(Guid resultadoSorteioId, Guid perguntaQuizId)
        {
            return await _context.PerguntasAmigoSecreto
                .AnyAsync(p => p.ResultadoSorteioId == resultadoSorteioId
                    && p.PerguntaQuizId == perguntaQuizId
                    && p.Status == StatusPerguntaEnum.Ativa);
        }
    }
}
