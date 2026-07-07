using ConectElo.Domain.Areas.Base;
using ConectElo.Domain.Areas.Dinamicas.Enuns;

namespace ConectElo.Domain.Areas.Dinamicas.Entities
{
    /// <summary>
    /// Instância de uma pergunta do quiz feita pelo presenteador ao seu recebedor dentro de um par sorteado.
    /// Preserva o anonimato: tudo pende do <see cref="ResultadoSorteioId"/>, nunca do usuário.
    /// </summary>
    public class PerguntaAmigoSecreto : EntityBase
    {
        public Guid ResultadoSorteioId { get; set; }
        public virtual ResultadoSorteio ResultadoSorteio { get; set; }

        public Guid PerguntaQuizId { get; set; }
        public virtual PerguntaQuiz PerguntaQuiz { get; set; }

        public Guid? OpcaoRespostaId { get; set; }
        public virtual OpcaoQuiz? OpcaoResposta { get; set; }

        public StatusPerguntaEnum Status { get; set; } = StatusPerguntaEnum.Ativa;

        public DateTime PerguntadaEm { get; set; }
        public DateTime? RespondidaEm { get; set; }
    }
}
