using ConectElo.Domain.Areas.Social.Enuns;

namespace ConectElo.Application.Areas.Social.DTOs
{
    public class RegistrarUsuarioDto
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string password { get; set; }
        public string? CPF { get; set; }
        public string? FotoPerfillUrl { get; set; }
        public DateOnly DataNascimento { get; set; }
        public GeneroEnum Genero { get; set; }
        public string? Bio { get; set; }
    }
}
