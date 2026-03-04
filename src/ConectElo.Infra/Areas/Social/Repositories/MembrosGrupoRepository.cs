using ConectElo.Domain.Areas.Social.Entities;
using ConectElo.Domain.Areas.Social.InterfacesRepository;
using ConectElo.Infra.Areas.Base;
using ConectElo.Infra.Data;

namespace ConectElo.Infra.Areas.Social.Repositories
{
    public class MembrosGrupoRepository : RepositoryGeneric<MembrosGrupo>, IMembrosGrupoRepository
    {
        private readonly AppDbContext _context;

        public MembrosGrupoRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
