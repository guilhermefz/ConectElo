using ConectElo.Domain.Areas.Base.Interfaces;
using ConectElo.Domain.Areas.Dinamicas.Entities;
using ConectElo.Domain.Areas.Eventos.InterfacesRepository;
using ConectElo.Infra.Areas.Base;
using ConectElo.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace ConectElo.Infra.Areas.Eventos.Repositories
{
    public class ItensListaDesejosRepository : RepositoryGeneric<ItensListaDesejos>, IItensListaDesejosRepository
    {
        private readonly AppDbContext _context;

        public ItensListaDesejosRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<ItensListaDesejos?> BuscarPorId(Guid id)
        {
            return await _context.ItensListaDesejos
                .Include(i => i.ReservadoPor)
                .FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task RemoverPorId(Guid id)
        {
            var item = await _context.ItensListaDesejos.FindAsync(id);
            if (item is not null)
            {
                _context.ItensListaDesejos.Remove(item);
                await _context.SaveChangesAsync();
            }
        }
    }
}
