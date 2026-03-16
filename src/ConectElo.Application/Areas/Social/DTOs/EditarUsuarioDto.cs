using ConectElo.Domain.Areas.Social.Enuns;

namespace ConectElo.Application.Areas.Social.DTOs
{
    public class EditarUsuarioDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string? CPF { get; set; }
        public string? FotoPerdilUrl { get; set; }
        public DateOnly DataNascimento { get; set; }
        public GeneroEnum Genero { get; set; }
        public string? Bio { get; set; }
    }
}
