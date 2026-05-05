using ConectElo.Domain.Areas.Social.Enuns;

namespace ConectElo.Application.Areas.Social.DTOs.Perfil
{
    public class PerfilUsuarioDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string? Bio {  get; set; }
        public string? FotoPerfilUrl { get; set; }
        public DateOnly DataNascimento { get; set; }
        public GeneroEnum Genero { get; set; }
    }
}
