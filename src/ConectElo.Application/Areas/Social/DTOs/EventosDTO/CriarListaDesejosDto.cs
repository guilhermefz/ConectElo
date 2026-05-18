namespace ConectElo.Application.Areas.Social.DTOs.EventosDTO
{
    public class CriarListaDesejosDto
    {
        public string Titulo { get; set; }
        public List<CriarItemListaDesejosDto> Itens { get; set; } = new();
    }
}
