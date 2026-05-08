namespace ConectElo.Application.Areas.Social.DTOs
{
    public class GrupoExibicaoDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public Guid MuralId { get; set; }
        public string ImgGrupo { get; set; }

        public List<MembroGrupoExibicaoDto> Membros { get; set; }
    }
}
