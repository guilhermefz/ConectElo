using ConectElo.Domain.Areas.Social.Entities;
using ConectElo.Domain.Areas.Social.InterfacesRepository;
using ConectElo.Infra.Areas.Base;
using ConectElo.Infra.Data;

namespace ConectElo.Infra.Areas.Social.Repositories
{
    public class MuralRepository : RepositoryGeneric<Mural>, IMuralRepository
    {
        private readonly AppDbContext _context;

        public MuralRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
