namespace ConectElo.Application.Areas.Social.DTOs
{
    public class CriarGrupoDto
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public bool Privado { get; set; }
        public Guid ProprietarioId { get; set; }
        public string? ImgGrupo { get; set; }
    }
}
