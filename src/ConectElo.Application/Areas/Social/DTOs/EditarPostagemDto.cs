using ConectElo.Domain.Areas.Social.Entities;

namespace ConectElo.Application.Areas.Social.DTOs
{
    public class EditarPostagemDto
    {
        public Guid Id { get; set; }
        public string Conteudo { get; set; }
        public Guid UsuarioId { get; set; }
    }
}
