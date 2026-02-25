using ConectElo.Domain.Areas.Base;

namespace ConectElo.Domain.Areas.Dinamicas.Entities
{
    public class ListaDesejos : EntityBase
    {
        public string Titulo { get; set; }
        public virtual ICollection<ItensListaDesejos> Itens { get; set; } = new List<ItensListaDesejos>();
    }
}
