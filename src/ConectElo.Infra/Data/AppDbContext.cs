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
        public DbSet<PerguntaQuiz> PerguntasQuiz { get; set; }
        public DbSet<OpcaoQuiz> OpcoesQuiz { get; set; }
        public DbSet<PerguntaAmigoSecreto> PerguntasAmigoSecreto { get; set; }


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

            modelBuilder.Entity<OpcaoQuiz>()
                .HasOne(o => o.PerguntaQuiz)
                .WithMany(p => p.Opcoes)
                .HasForeignKey(o => o.PerguntaQuizId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PerguntaAmigoSecreto>()
                .HasOne(p => p.ResultadoSorteio)
                .WithMany()
                .HasForeignKey(p => p.ResultadoSorteioId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PerguntaAmigoSecreto>()
                .HasOne(p => p.PerguntaQuiz)
                .WithMany()
                .HasForeignKey(p => p.PerguntaQuizId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PerguntaAmigoSecreto>()
                .HasOne(p => p.OpcaoResposta)
                .WithMany()
                .HasForeignKey(p => p.OpcaoRespostaId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PerguntaQuiz>().HasData(PerguntasQuizPadrao());
            modelBuilder.Entity<OpcaoQuiz>().HasData(OpcoesQuizPadrao());
        }

        // Catálogo fixo do quiz de amigo secreto. A ordem define o Id determinístico (seed).
        private static readonly (string Texto, (string? Emoji, string Texto)[] Opcoes)[] QuizSeed =
        {
            ("Qual presente te deixa mais feliz?", new (string?, string)[]
            {
                ("📚", "Algo útil"), ("😂", "Algo engraçado"), ("✨", "Algo criativo"),
                ("❤️", "Algo com significado"), ("🎉", "Surpresa total")
            }),
            ("Você prefere ganhar…", new (string?, string)[]
            {
                ("🍫", "Consumível"), ("🧸", "Objeto"), ("🎟️", "Experiência"), ("💌", "Algo feito à mão")
            }),
            ("Qual categoria mais combina com você?", new (string?, string)[]
            {
                ("📚", "Livros"), ("☕", "Café/Chá"), ("🎮", "Games"),
                ("🎧", "Música"), ("👕", "Roupas"), ("🏠", "Casa")
            }),
            ("Seu tipo de presente ideal é…", new (string?, string)[]
            {
                (null, "Pequeno e útil"), (null, "Diferente"), (null, "Premium"),
                (null, "Engraçado"), (null, "Minimalista"), ("🎨", "Estilo")
            }),
            ("Qual sua estética?", new (string?, string)[]
            {
                ("⚫", "Minimalista"), ("🌈", "Colorido"), ("🪵", "Natural"), ("✨", "Moderno"), ("🎮", "Geek")
            }),
            ("Escolha uma cor", new (string?, string)[]
            {
                ("⚫", "Preto"), ("⚪", "Branco"), ("🔵", "Azul"),
                ("🟢", "Verde"), ("🔴", "Vermelho"), ("🟣", "Roxo")
            }),
            ("Você é mais…", new (string?, string)[]
            {
                ("🏠", "Caseiro"), ("✈️", "Explorador"), ("☕", "Relax"), ("🏃", "Ativo"), ("🍕", "Hábitos")
            }),
            ("Seu momento favorito do dia", new (string?, string)[]
            {
                ("🌅", "Manhã"), ("☀️", "Tarde"), ("🌙", "Noite"), ("🌃", "Madrugada")
            }),
            ("Final de semana perfeito", new (string?, string)[]
            {
                ("🎬", "Filmes"), ("🎮", "Jogos"), ("🍽️", "Comer fora"), ("📚", "Ler"), ("💤", "Descansar")
            }),
            ("O que você menos gosta de ganhar?", new (string?, string)[]
            {
                (null, "Perfume"), (null, "Roupa"), (null, "Decoração"),
                (null, "Tecnologia"), (null, "Vale-presente")
            }),
            ("Qual energia combina com você?", new (string?, string)[]
            {
                (null, "Golden retriever"), (null, "Gato"), (null, "Coruja"), (null, "Capivara"), (null, "Panda")
            })
        };

        private static Guid PerguntaQuizId(int indice) => Guid.Parse($"b1f1c0d0-0000-0000-0000-{(indice + 1):x12}");
        private static Guid OpcaoQuizId(int ordinal) => Guid.Parse($"b2f2c0d0-0000-0000-0000-{ordinal:x12}");

        private static IEnumerable<PerguntaQuiz> PerguntasQuizPadrao()
        {
            return QuizSeed.Select((q, i) => new PerguntaQuiz
            {
                Id = PerguntaQuizId(i),
                Texto = q.Texto,
                Ativa = true
            });
        }

        private static IEnumerable<OpcaoQuiz> OpcoesQuizPadrao()
        {
            var opcoes = new List<OpcaoQuiz>();
            var ordinal = 0;

            for (var i = 0; i < QuizSeed.Length; i++)
            {
                var perguntaId = PerguntaQuizId(i);
                var itens = QuizSeed[i].Opcoes;

                for (var j = 0; j < itens.Length; j++)
                {
                    ordinal++;
                    opcoes.Add(new OpcaoQuiz
                    {
                        Id = OpcaoQuizId(ordinal),
                        PerguntaQuizId = perguntaId,
                        Emoji = itens[j].Emoji,
                        Texto = itens[j].Texto,
                        Ordem = j + 1
                    });
                }
            }

            return opcoes;
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
