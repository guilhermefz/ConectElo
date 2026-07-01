using ConectElo.Domain.Areas.Comunicacao.Entities;
using ConectElo.Domain.Areas.Dinamicas.Entities;
using ConectElo.Domain.Areas.Eventos.Entities;
using ConectElo.Domain.Areas.Geral.Entities;
using ConectElo.Domain.Areas.Social.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ConectElo.Infra.Data
{
    public class AppDbContext : IdentityDbContext<Usuario, IdentityRole<Guid>, Guid>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Mensagem> Mensagens { get; set; }
        public DbSet<Notificacoes> Notificacoes { get; set; }
        public DbSet<AmigoSecretoEvento> AmigoSecretoEventos { get; set; }
        public DbSet<AniversarioEvento> AniversarioEventos { get; set; }
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
        public DbSet<ResultadoSorteio> ResultadoSorteios { get; set; }
        public DbSet<MensagemAnonima> MensagensAnonimas { get; set; }
        public DbSet<Interesse> Interesses { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Postagens>()
               .HasIndex(p => new { p.MuralId, p.DataPostagem })
               .HasDatabaseName("IX_Postagens_MuralId_DataPostagem");

            modelBuilder.Entity<MembrosGrupo>()
                .HasIndex(m => m.UsuarioId)
                .HasDatabaseName("IX_MembrosGrupo_UsuarioId");

            modelBuilder.Entity<Grupo>()
                .HasIndex(g => g.CodigoConvite)
                .IsUnique()
                .HasFilter("\"CodigoConvite\" IS NOT NULL")
                .HasDatabaseName("IX_Grupos_CodigoConvite");

            modelBuilder.Entity<Evento>()
                .HasOne(e => e.CriadorEvento)
                .WithMany()
                .HasForeignKey(e => e.Criador)
                .IsRequired(false);

            modelBuilder.Entity<Interesse>()
                .HasData(InteressesPadrao());
        }

        private static IEnumerable<Interesse> InteressesPadrao()
        {
            string[] nomes =
            {
                "Café", "Sushi", "Cozinhar", "Vinho", "Música",
                "Filmes", "Livros", "Games", "Academia", "Corrida",
                "Futebol", "Boxe", "Praia", "Viagens", "Fotografia",
                "Arte", "Museus", "Tecnologia", "Programação", "Carros",
                "Moda", "Tatuagens", "Cachorros", "Jardinagem", "Jogos de tabuleiro",
                "Podcasts", "Astrologia", "Festivais", "Baladas", "Stand-up Comedy"
            };

            return nomes.Select((nome, i) => new Interesse
            {
                Id = Guid.Parse($"a1f1c0d0-0000-0000-0000-{(i + 1):x12}"),
                Nome = nome
            });
        }
    }
}
