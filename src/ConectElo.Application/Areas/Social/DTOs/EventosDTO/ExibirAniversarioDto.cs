using ConectElo.Application.Areas.EventosArea.DTOs;

namespace ConectElo.Application.Areas.Social.DTOs.EventosDTO
{
    public class ExibirAniversarioDto : ExibirEventoDto
    {
        public string NomeAniversariante { get; set; }
        public int? Idade { get; set; }
        public ExibirListaDesejosDto? ListaDesejos { get; set; }
    }
}
