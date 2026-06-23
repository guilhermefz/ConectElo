using ConectElo.Domain.Areas.Dinamicas.Entities;
using ConectElo.Domain.Areas.Eventos.InterfacesRepository;
using ConectElo.Infra.Areas.Base;
using ConectElo.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace ConectElo.Infra.Areas.Eventos.Repositories
{
    public class MensagemAnonimaRepository : RepositoryGeneric<MensagemAnonima>, IMensagemAnonimaRepository
    {
        private readonly AppDbContext _context;

        public MensagemAnonimaRepository(AppDbContext context) : base(context) 
        {
            _context = context;
        }

        public async Task<List<MensagemAnonima>> ListarPorResultado(Guid resultadoSorteioId)
        {
            return await _context.MensagensAnonimas
                .Where(m => m.ResultadoSorteioId == resultadoSorteioId)
                .OrderBy(m => m.HorarioEnvio)
                .ToListAsync();
        }
    }
}
