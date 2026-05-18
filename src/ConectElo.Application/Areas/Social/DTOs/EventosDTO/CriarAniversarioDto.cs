namespace ConectElo.Application.Areas.Social.DTOs.EventosDTO
{
    public class CriarAniversarioDto
    {
        public string Titulo { get; set; }
        public string? Descricao { get; set; }
        public DateTime? DataInicio { get; set; }
        public string? Localizacao { get; set; }
        public Guid GrupoId { get; set; }
        public string NomeAniversariante { get; set; }
        public int? Idade { get; set; }
        public CriarListaDesejosDto? ListaDesejos { get; set; }
    }
}
