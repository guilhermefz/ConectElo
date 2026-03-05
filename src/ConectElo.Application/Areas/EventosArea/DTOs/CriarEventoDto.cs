using ConectElo.Domain.Areas.Eventos.Enuns;

namespace ConectElo.Application.Areas.EventosArea.DTOs
{
    public class CriarEventoDto
    {
        public string Titulo { get; set; }
        public string? Descricao { get; set; }
        public DateTime? DataInicio { get; set; }
        public string? Localizacao { get; set; }
        public Guid Criador { get; set; }
        public StatusEvento Status { get; set; }
        public TipoEventoEnum TipoEvento { get; set; }
        public Guid GrupoId { get; set; }
    }
}
