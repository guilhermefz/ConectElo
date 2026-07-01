namespace ConectElo.Application.Areas.AmigoSecreto.DTOs
{
    /// <summary>Pergunta em aberto na visão do presenteador (ocupa um slot). Resposta nula = aguardando.</summary>
    public class PerguntaAtivaDto
    {
        public Guid PerguntaAmigoSecretoId { get; set; }
        public Guid PerguntaQuizId { get; set; }
        public string Texto { get; set; }
        public OpcaoQuizDto? Resposta { get; set; }
        public DateTime PerguntadaEm { get; set; }
        public DateTime? RespondidaEm { get; set; }
    }
}
