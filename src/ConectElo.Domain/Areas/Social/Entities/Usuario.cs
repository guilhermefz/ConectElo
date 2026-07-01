using ConectElo.Domain.Areas.Base;
using ConectElo.Domain.Areas.Social.Enuns;
using Microsoft.AspNetCore.Identity;

namespace ConectElo.Domain.Areas.Social.Entities
{
    public class Usuario : IdentityUser<Guid>
    {
        public Usuario()
        {
            Id = Guid.NewGuid();
        }

        public string Nome { get; set; }
        public string? CPF { get; set; }
        public string? FotoPerdilUrl { get; set; }
        public DateOnly DataNascimento { get; set; }
        public DateTime DataCriacaoConta { get; set; } = DateTime.UtcNow;
        public GeneroEnum Genero { get; set; }
        public DateTime UltimaAtualizacao { get; set; } = DateTime.UtcNow;
        public bool UsuarioAtivo { get; set; }
        public string? Bio {  get; set; }
        public DateTime? DataDelecao { get; set; }
        public virtual ICollection<Interesse> Interesses { get; set; } = new List<Interesse>();
    }
}
