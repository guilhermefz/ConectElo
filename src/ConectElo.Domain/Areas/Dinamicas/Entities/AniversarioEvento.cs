using ConectElo.Domain.Areas.Eventos.Entities;

namespace ConectElo.Domain.Areas.Dinamicas.Entities
{
    public class AniversarioEvento : Evento
    {
        public string NomeAniversariante { get; set; }
        public int? Idade { get; set; }
        public Guid? ListaDesejosId { get; set; }
        public virtual ListaDesejos? ListaDesejos { get; set; }
    }
}
