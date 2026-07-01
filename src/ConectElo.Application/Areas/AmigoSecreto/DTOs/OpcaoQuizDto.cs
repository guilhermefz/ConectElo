namespace ConectElo.Application.Areas.AmigoSecreto.DTOs
{
    public class OpcaoQuizDto
    {
        public Guid Id { get; set; }
        public string? Emoji { get; set; }
        public string Texto { get; set; }
        public int Ordem { get; set; }
    }
}
