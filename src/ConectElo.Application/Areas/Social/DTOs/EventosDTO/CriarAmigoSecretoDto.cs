namespace ConectElo.Application.Areas.Social.DTOs.EventosDTO
{
    public class CriarAmigoSecretoDto
    {
        public string Titulo { get; set; }
        public string? Descricao { get; set; }
        public DateTime? DataInicio { get; set; }
        public string? Localizacao { get; set; }
        public Guid GrupoId { get; set; }
        public double ValorMinimo { get; set; }
        public DateTime DataSorteio { get; set; }
    }
}
