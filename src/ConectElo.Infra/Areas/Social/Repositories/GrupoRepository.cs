using ConectElo.Domain.Areas.Social.Entities;
using ConectElo.Domain.Areas.Social.InterfacesRepository;
using ConectElo.Infra.Areas.Base;
using ConectElo.Infra.Data;

namespace ConectElo.Infra.Areas.Social.Repositories
{
    public class GrupoRepository : RepositoryGeneric<Grupo>, IGrupoRepository
    {
        private readonly AppDbContext _context;

        public GrupoRepository(AppDbContext context) : base(context) 
        {
            _context = context;
        }

    }
}
