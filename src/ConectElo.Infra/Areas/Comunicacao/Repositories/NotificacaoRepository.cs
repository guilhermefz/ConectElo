using ConectElo.Domain.Areas.Comunicacao.Entities;
using ConectElo.Domain.Areas.Comunicacao.InterfacesRepository;
using ConectElo.Infra.Areas.Base;
using ConectElo.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace ConectElo.Infra.Areas.Comunicacao.Repositories
{
    public class NotificacaoRepository : RepositoryGeneric<Notificacoes>, INotificacoesRepository
    {
        private readonly AppDbContext _context;

        public NotificacaoRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Notificacoes>> ListarPorUsuario(Guid usuarioId)
        {
            return await _context.Notificacoes
                .Where(n => n.UsuarioId == usuarioId)
                .OrderByDescending(n => n.DataEnvio)
                .ToListAsync();
        }

        public async Task MarcarTodasComoLidasAsync(Guid usuarioId)
        {
            await _context.Notificacoes
                .Where(n => n.UsuarioId == usuarioId && !n.NotificacaoLida)
                .ExecuteUpdateAsync(s => s.SetProperty(n => n.NotificacaoLida, true));
        }
    }
}
