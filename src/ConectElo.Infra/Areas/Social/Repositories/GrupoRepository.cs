using ConectElo.Domain.Areas.Social.Entities;
using ConectElo.Domain.Areas.Social.InterfacesRepository;
using ConectElo.Infra.Areas.Base;
using ConectElo.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace ConectElo.Infra.Areas.Social.Repositories
{
    public class GrupoRepository : RepositoryGeneric<Grupo>, IGrupoRepository
    {
        private readonly AppDbContext _context;

        public GrupoRepository(AppDbContext context) : base(context) 
        {
            _context = context;
        }

        public async Task<Grupo?> ObterGrupoComInclude(Guid id)
        {
            return await _context.Grupos
                .Include(g => g.Membros)
                    .ThenInclude(m => m.Usuario)
                .AsNoTracking()
                .FirstOrDefaultAsync(g => g.Id == id);
        }

        public async Task<IEnumerable<Grupo>> BuscarPorUsuario(Guid usuarioId)
        {
            return await _context.Grupos
                .Include(g => g.Membros)
                .AsNoTracking()
                .Where(g => g.Membros.Any(m => m.UsuarioId == usuarioId))
                .ToListAsync();
        }
    }
}
