namespace ConectElo.Application.Areas.Social.DTOs.EventosDTO
{
    public class ExibirListaDesejosDto
    {
        public Guid Id { get; set; }
        public string Titulo { get; set; }
        public List<ExibirItemListaDesejosDto> Itens { get; set; } = new();

    }
}
