namespace ConectElo.Application.Areas.Social.DTOs.EventosDTO
{
    public class ExibirItemListaDesejosDto
    {
        public Guid Id { get; set; }
        public string Descricao { get; set; }
        public string? UrlReference { get; set; }
        public Guid? ReservadoPorId { get; set; }
    }
}
