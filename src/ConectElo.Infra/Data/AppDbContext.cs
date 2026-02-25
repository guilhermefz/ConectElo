using ConectElo.Domain.Areas.Comunicacao.Entities;
using ConectElo.Domain.Areas.Dinamicas.Entities;
using ConectElo.Domain.Areas.Eventos.Entities;
using ConectElo.Domain.Areas.Geral.Entities;
using ConectElo.Domain.Areas.Social.Entities;
using Microsoft.EntityFrameworkCore;

namespace ConectElo.Infra.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }
        
            public DbSet<Usuario> Usuarios { get; set; }
            public DbSet<Mensagem> Mensagens { get; set; }
            public DbSet<Notificacoes> Notificacoes { get; set; }
            public DbSet<AmigoSecretoEvento> AmigoSecretoEventos { get; set; }
            public DbSet<ItensListaDesejos> ItensListaDesejos { get; set; }
            public DbSet<ListaDesejos> ListaDesejos { get; set; }
            public DbSet<ConviteEvento> ConviteEventos { get; set; }
            public DbSet<Evento> Eventos { get; set; }
            public DbSet<BanimentoGrupo> BanimentoGrupo { get; set; }
            public DbSet<ConfirmacaoEvento> ConfirmacaoEventos { get; set; }
            public DbSet<SolicitacaoEntrada> SolicitacaoEntrada {  get; set; }
            public DbSet<GaleriaFotos> GaleriaFotos { get; set; }
            public DbSet<Grupo> Grupos { get; set; }
            public DbSet<MembrosGrupo> membrosGrupos { get; set; }
            public DbSet<Mural> Mural { get; set; }
            public DbSet<Postagens> Postagens { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
