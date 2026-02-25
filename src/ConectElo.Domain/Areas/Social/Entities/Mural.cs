using ConectElo.Domain.Areas.Base;
using ConectElo.Domain.Areas.Social.Enuns;

namespace ConectElo.Domain.Areas.Social.Entities
{
    public class Mural : EntityBase
    {
        TipoMuralEnum Tipo { get; set; }
    }
}
