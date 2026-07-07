using ConectElo.Domain.Areas.Base;

namespace ConectElo.Domain.Areas.Dinamicas.Entities
{
    /// <summary>
    /// Opção de resposta de uma <see cref="PerguntaQuiz"/> (dados de referência, populados via seed).
    /// </summary>
    public class OpcaoQuiz : EntityBase
    {
        public Guid PerguntaQuizId { get; set; }
        public virtual PerguntaQuiz PerguntaQuiz { get; set; }

        public string? Emoji { get; set; }
        public string Texto { get; set; }
        public int Ordem { get; set; }
    }
}
