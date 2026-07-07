using ConectElo.Domain.Areas.Social.Entities;
using ConectElo.Domain.Areas.Social.InterfacesRepository;
using ConectElo.Infra.Areas.Base;
using ConectElo.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace ConectElo.Infra.Areas.Social.Repositories
{
    public class InteresseRepository : RepositoryGeneric<Interesse>, IInteresseRepository
    {
        public InteresseRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<List<Interesse>> ListarTodos()
        {
            return await _dbSet.AsNoTracking()
                .OrderBy(i => i.Nome)
                .ToListAsync();
        }

        public async Task<List<Interesse>> ListarPorIds(List<Guid> ids)
        {
            return await _dbSet
                .Where(i => ids.Contains(i.Id))
                .ToListAsync();
        }

        public async Task<Usuario?> ObterUsuarioComInteresses(Guid usuarioId)
        {
            return await _context.Users
                .Include(u => u.Interesses)
                .FirstOrDefaultAsync(u => u.Id == usuarioId);
        }
    }
}
