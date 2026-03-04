namespace ConectElo.Application.Areas.Social.DTOs
{
    public class CriarPostagemDto
    {
        public string Conteudo { get; set; }
        public Guid UsuarioId { get; set; }
        public Guid MuralId { get; set; }
    }
}
