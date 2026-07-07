namespace ConectElo.Application.Areas.AmigoSecreto.DTOs
{
    /// <summary>Pergunta que o recebedor precisa responder (ou pode alterar a resposta).</summary>
    public class PerguntaRecebidaDto
    {
        public Guid PerguntaAmigoSecretoId { get; set; }
        public string Texto { get; set; }
        public List<OpcaoQuizDto> Opcoes { get; set; } = new();
        public Guid? OpcaoRespostaId { get; set; }
    }
}
