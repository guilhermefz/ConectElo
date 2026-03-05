using ConectElo.Domain.Areas.Eventos.Entities;
using ConectElo.Domain.Areas.Eventos.InterfacesRepository;
using ConectElo.Infra.Areas.Base;
using ConectElo.Infra.Data;

namespace ConectElo.Infra.Areas.Eventos.Repositories
{
    public class EventoRepository : RepositoryGeneric<Evento>, IEventoRepository
    {
        private readonly AppDbContext _context;

        public EventoRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
