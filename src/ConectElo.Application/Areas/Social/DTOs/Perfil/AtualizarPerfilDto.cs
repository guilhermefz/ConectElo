using ConectElo.Domain.Areas.Social.Enuns;
using System.Globalization;

namespace ConectElo.Application.Areas.Social.DTOs.Perfil
{
    public class AtualizarPerfilDto
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string? Bio { get; set; }
        public DateOnly DataNascimento { get; set; }
        public GeneroEnum Genero { get; set; }
    }
}
