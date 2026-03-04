using ConectElo.Domain.Areas.Social.Entities;
using ConectElo.Domain.Areas.Social.InterfacesRepository;
using ConectElo.Infra.Areas.Base;
using ConectElo.Infra.Data;

namespace ConectElo.Infra.Areas.Social.Repositories
{
    public class PostagemRepository : RepositoryGeneric<Postagens>, IPostagemRepository
    {
        private readonly AppDbContext _context;

        public PostagemRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
