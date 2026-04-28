namespace ConectElo.Application.Areas.Social.DTOs
{
    public class FeedPostagemDto
    {
        public Guid Id { get; set; }
        public string Conteudo { get; set; }
        public DateTime DataPostagem { get; set; }
        public Guid UsuarioId { get; set; }
        public string NomeAutor { get; set; }
        public Guid MuralId { get; set; }
        public Guid GrupoId { get; set; }
        public string NomeGrupo { get; set; }
    }
}
