using ConectElo.Domain.Areas.Base;
using ConectElo.Domain.Areas.Social.Enuns;

namespace ConectElo.Domain.Areas.Social.Entities
{
    public class Usuario : EntityBase
    {
        public string Nome { get; set; }
        public string? CPF { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string? FotoPerdilUrl { get; set; }
        public DateOnly DataNascimento { get; set; }
        public DateTime DataCriacaoConta { get; set; }
        public GeneroEnum Genero { get; set; }
        public string SenhaHash { get; set; }
        public DateTime UltimaAtualizacao { get; set; }
        public bool UsuarioAtivo { get; set; }
        public string? Bio {  get; set; }
        public DateTime? DataDelecao { get; set; }
    }
}
