using ConectElo.Domain.Areas.Base;
using ConectElo.Domain.Areas.Social.Entities;

namespace ConectElo.Domain.Areas.Dinamicas.Entities
{
    public class ItensListaDesejos : EntityBase
    {
        public string Descricao { get; set; }
        public string UrlReference { get; set; }
        public Guid ListaDesejosId { get; set; }
        public virtual ListaDesejos ListaDesejos { get; set; } = null;
        public Guid ReservadoPorId { get; set; }
        public virtual Usuario? ReservadoPor { get; set; }
    }
}
