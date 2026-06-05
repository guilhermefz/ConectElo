using ConectElo.Domain.Areas.Geral.Enuns;

namespace ConectElo.Application.Areas.Home.DTOs
{
    public class ProximoEventoDto
    {
        public Guid Id { get; set; }
        public string Titulo { get; set; }
        public string NomeGrupo { get; set; }
        public DateTime? DataInicio { get; set; }
        public string? Localizacao { get; set; }
        public string? FotoCapaUrl { get; set; }
        public int DiasRestantes { get; set; }
        public StatusConfirmacaoEventoEnum? MinhaConfirmacao { get; set; }
    }
}
