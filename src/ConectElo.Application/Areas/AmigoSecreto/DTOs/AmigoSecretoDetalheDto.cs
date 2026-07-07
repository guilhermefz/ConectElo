using ConectElo.Application.Areas.Social.DTOs.EventosDTO;
using ConectElo.Application.Areas.Social.DTOs.Perfil;
using ConectElo.Domain.Areas.Social.Enuns;

namespace ConectElo.Application.Areas.AmigoSecreto.DTOs
{
    /// <summary>
    /// Estado completo da tela de detalhe do amigo secreto na visão do presenteador:
    /// dados do recebedor, logística do evento e o quiz. Não expõe a identidade (id/email) do recebedor.
    /// </summary>
    public class AmigoSecretoDetalheDto
    {
        public Guid ResultadoSorteioId { get; set; }

        // Recebedor
        public string NomeRecebedor { get; set; }
        public string? FotoRecebedor { get; set; }
        public string? Bio { get; set; }
        public int? Idade { get; set; }
        public GeneroEnum Genero { get; set; }
        public List<InteresseDto> Interesses { get; set; } = new();
        public ExibirListaDesejosDto? ListaDesejos { get; set; }

        // Logística
        public double Valor { get; set; }
        public DateTime DataSorteio { get; set; }

        // Quiz
        public int SlotsTotais { get; set; }
        public int SlotsUsados { get; set; }
        public List<PerguntaAtivaDto> PerguntasAtivas { get; set; } = new();
        public List<PerguntaCatalogoDto> PerguntasDisponiveis { get; set; } = new();
    }
}
