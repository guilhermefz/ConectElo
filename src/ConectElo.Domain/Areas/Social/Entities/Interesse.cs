using ConectElo.Domain.Areas.Base;

namespace ConectElo.Domain.Areas.Social.Entities
{
    public class Interesse : EntityBase
    {
        public string Nome { get; set; }
        public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
    }
}
