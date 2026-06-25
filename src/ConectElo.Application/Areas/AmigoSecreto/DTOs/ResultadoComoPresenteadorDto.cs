using ConectElo.Application.Areas.Social.DTOs.EventosDTO;

namespace ConectElo.Application.Areas.AmigoSecreto.DTOs
{
    public class ResultadoComoPresenteadorDto
    {
        public Guid ResultadoSorteioId { get; set; }
        public string NomeRecebedor { get; set; }
        public string? FotoRecebedor { get; set; }
        public ExibirListaDesejosDto? ListaDesejos { get; set; }
    }
}
