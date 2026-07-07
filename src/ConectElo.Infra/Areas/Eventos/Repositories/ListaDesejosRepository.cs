using ConectElo.Domain.Areas.Dinamicas.Entities;
using ConectElo.Domain.Areas.Eventos.InterfacesRepository;
using ConectElo.Infra.Areas.Base;
using ConectElo.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace ConectElo.Infra.Areas.Eventos.Repositories
{
    public class ListaDesejosRepository : RepositoryGeneric<ListaDesejos>, IListaDesejosRepository
    {
        public ListaDesejosRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<ListaDesejos?> BuscarPorEventoEUsuario(Guid eventoId, Guid usuarioId)
        {
            return await _context.ListaDesejos
                .Include(l => l.Itens)
                .FirstOrDefaultAsync(l => l.EventoId == eventoId && l.UsuarioId == usuarioId);
        }
    }
}
