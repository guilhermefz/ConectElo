using ConectElo.Domain.Areas.Base;

namespace ConectElo.Domain.Areas.Social.Entities
{
    public class Postagens : EntityBase
    {
        public string Conteudo { get; set; }
        public DateTime DataPostagem { get; set; }
        public Guid UsuarioId { get; set; }
        public virtual Usuario Autor {  get; set; }
        public Guid MuralId { get; set; }
    }
}
