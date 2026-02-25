using ConectElo.Domain.Areas.Base;
using ConectElo.Domain.Areas.Eventos.Enuns;
using ConectElo.Domain.Areas.Social.Entities;

namespace ConectElo.Domain.Areas.Eventos.Entities
{
    public class Evento : EntityBase
    {
        public string Titulo { get; set; }
        public string? Descricao { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataInicio { get; set; }
        public DateTime? DataDelecao { get; set; }
        public string? Localizacao { get; set; }
        public Guid Criador {  get; set; }
        public virtual Usuario? CriadorEvento { get; set;  }
        public StatusEvento Status { get; set; }
        public TipoEventoEnum TipoEvento { get; set; }
        public Guid GrupoId { get; set; }
    }
}
