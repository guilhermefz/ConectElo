using ConectElo.Domain.Areas.Social.Entities;

namespace ConectElo.Application.Areas.Social.DTOs
{
    public class ExibirPostagemDto
    {
        public Guid id { get; set; }
        public string Conteudo { get; set; }
        public DateTime DataPostagem { get; set; }
        public Guid UsuarioId { get; set; }
        public Guid MuralId { get; set; }
    }
}
