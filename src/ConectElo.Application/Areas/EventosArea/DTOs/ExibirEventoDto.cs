using ConectElo.Domain.Areas.Eventos.Enuns;

namespace ConectElo.Application.Areas.EventosArea.DTOs
{
    public class ExibirEventoDto
    {
        public Guid Id { get; set; }
        public string Titulo { get; set; }
        public string? Descricao { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataInicio { get; set; }
        public DateTime? DataDelecao { get; set; }
        public string? Localizacao { get; set; }
        public Guid Criador { get; set; }
        public StatusEvento Status { get; set; }
        public TipoEventoEnum TipoEvento { get; set; }
        public Guid GrupoId { get; set; }
        public string? FotoCapaUrl { get; set; }
        public string? CriadorNome { get; set; }
    }
}
