namespace ConectElo.Application.Areas.AmigoSecreto.DTOs
{
    public class PerguntarQuizDto
    {
        public Guid PerguntaQuizId { get; set; }
    }

    public class TrocarPerguntaQuizDto
    {
        public Guid NovaPerguntaQuizId { get; set; }
    }

    public class ResponderQuizDto
    {
        public Guid OpcaoId { get; set; }
    }
}
