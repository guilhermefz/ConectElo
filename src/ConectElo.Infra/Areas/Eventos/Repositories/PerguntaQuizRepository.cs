using ConectElo.Domain.Areas.Dinamicas.Entities;
using ConectElo.Domain.Areas.Eventos.InterfacesRepository;
using ConectElo.Infra.Areas.Base;
using ConectElo.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace ConectElo.Infra.Areas.Eventos.Repositories
{
    public class PerguntaQuizRepository : RepositoryGeneric<PerguntaQuiz>, IPerguntaQuizRepository
    {
        public PerguntaQuizRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<List<PerguntaQuiz>> ListarAtivasComOpcoes()
        {
            return await _context.PerguntasQuiz
                .AsNoTracking()
                .Include(p => p.Opcoes.OrderBy(o => o.Ordem))
                .Where(p => p.Ativa)
                .ToListAsync();
        }

        public async Task<PerguntaQuiz?> BuscarComOpcoes(Guid id)
        {
            return await _context.PerguntasQuiz
                .AsNoTracking()
                .Include(p => p.Opcoes.OrderBy(o => o.Ordem))
                .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
