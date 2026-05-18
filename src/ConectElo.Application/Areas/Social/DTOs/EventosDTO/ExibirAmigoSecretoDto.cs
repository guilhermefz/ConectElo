namespace ConectElo.Application.Areas.Social.DTOs.EventosDTO
{
    public class ExibirAmigoSecretoDto
    {
        public double ValorMinimo { get; set; }
        public DateTime DataSorteio { get; set; }
        public bool Sorteado { get; set; }
        public string? ResultadoSorteio { get; set; }
    }
}
