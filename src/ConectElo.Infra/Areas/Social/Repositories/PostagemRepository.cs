using ConectElo.Domain.Areas.Social.Entities;
using ConectElo.Domain.Areas.Social.InterfacesRepository;
using ConectElo.Infra.Areas.Base;
using ConectElo.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace ConectElo.Infra.Areas.Social.Repositories
{
    public class PostagemRepository : RepositoryGeneric<Postagens>, IPostagemRepository
    {
        private readonly AppDbContext _context;

        public PostagemRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Postagens>> ObterFeedDoUsuario(Guid usuarioId, int pagina, int tamanhoPagina)
        {
            var muralIds = await _context.membrosGrupos
                .Where(membro => membro.UsuarioId == usuarioId)
                    .Join(_context.Grupos,
                          membro => membro.GrupoId,
                          grupo => grupo.Id,
                          (membro, grupo) => grupo.MuralId)
                    .ToListAsync();

            return await _context.Postagens
                .Where(postagem => muralIds.Contains(postagem.MuralId))
                .Include(postagem => postagem.Autor)
                .OrderByDescending(postagem => postagem.DataPostagem)
                .Skip((pagina - 1) * tamanhoPagina)
                .Take(tamanhoPagina)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
