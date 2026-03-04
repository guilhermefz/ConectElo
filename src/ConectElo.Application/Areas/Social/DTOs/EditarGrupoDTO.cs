namespace ConectElo.Application.Areas.Social.DTOs
{
    public class EditarGrupoDto
    {
        public Guid id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public bool Privado { get; set; }
        public string? ImgGrupo { get; set; }
    }
}
