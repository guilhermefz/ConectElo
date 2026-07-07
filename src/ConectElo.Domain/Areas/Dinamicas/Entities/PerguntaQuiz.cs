using ConectElo.Domain.Areas.Base;

namespace ConectElo.Domain.Areas.Dinamicas.Entities
{
    /// <summary>
    /// Pergunta fixa do catálogo de quiz do amigo secreto (dados de referência, populados via seed).
    /// </summary>
    public class PerguntaQuiz : EntityBase
    {
        public string Texto { get; set; }
        public bool Ativa { get; set; } = true;

        public virtual ICollection<OpcaoQuiz> Opcoes { get; set; } = new List<OpcaoQuiz>();
    }
}
