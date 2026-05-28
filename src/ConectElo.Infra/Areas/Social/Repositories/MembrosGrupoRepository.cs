using ConectElo.Domain.Areas.Social.Entities;
using ConectElo.Domain.Areas.Social.InterfacesRepository;
using ConectElo.Infra.Areas.Base;
using ConectElo.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace ConectElo.Infra.Areas.Social.Repositories
{
    public class MembrosGrupoRepository : RepositoryGeneric<MembrosGrupo>, IMembrosGrupoRepository
    {
        private readonly AppDbContext _context;

        public MembrosGrupoRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> VerificarMembroASync(Guid grupoId, Guid usuarioId)
        {
            return await _context.Set<MembrosGrupo>()
                .AnyAsync(membro => membro.GrupoId == grupoId && membro.UsuarioId == usuarioId);
        }

        public async Task<MembrosGrupo?> BuscarMembroPorGrupoEUsuario(Guid grupoId, Guid usuarioId)
        {
            return await _context.membrosGrupos.FirstOrDefaultAsync(m => m.GrupoId == grupoId && m.UsuarioId ==usuarioId);
        }
    }
}
