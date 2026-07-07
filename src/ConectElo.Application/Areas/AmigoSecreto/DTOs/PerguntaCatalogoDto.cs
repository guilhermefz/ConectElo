namespace ConectElo.Application.Areas.AmigoSecreto.DTOs
{
    /// <summary>Pergunta do catálogo fixo, usada no seletor de perguntas do presenteador.</summary>
    public class PerguntaCatalogoDto
    {
        public Guid Id { get; set; }
        public string Texto { get; set; }
        public List<OpcaoQuizDto> Opcoes { get; set; } = new();
    }
}
