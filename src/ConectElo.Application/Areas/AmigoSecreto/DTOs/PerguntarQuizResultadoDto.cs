using ConectElo.Application.Areas.Comunicacao.DTOs;

namespace ConectElo.Application.Areas.AmigoSecreto.DTOs
{
    /// <summary>
    /// Resultado interno do envio de uma pergunta: a pergunta ativa (retornada ao presenteador)
    /// e o aviso gerado para o recebedor (usado apenas pelo controller para o push em tempo real).
    /// </summary>
    public class PerguntarQuizResultadoDto
    {
        public PerguntaAtivaDto Pergunta { get; set; }
        public ExibirNotificacaoDto NotificacaoRecebedor { get; set; }
    }
}
