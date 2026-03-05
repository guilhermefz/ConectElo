using ConectElo.Domain.Areas.Eventos.Enuns;

namespace ConectElo.Application.Areas.EventosArea.DTOs
{
    public class EditarEventoDto
    {
        public Guid Id { get; set; }
        public string Titulo { get; set; }
        public string? Descricao { get; set; }
        public DateTime? DataInicio { get; set; }
        public string? Localizacao { get; set; }
        public StatusEvento Status { get; set; }
        public TipoEventoEnum TipoEvento { get; set; }
    }
}
