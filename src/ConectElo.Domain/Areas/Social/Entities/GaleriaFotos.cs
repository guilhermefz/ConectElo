using ConectElo.Domain.Areas.Base;

namespace ConectElo.Domain.Areas.Social.Entities
{
    public class GaleriaFotos : EntityBase
    {
        public string UrlFoto { get; set; }
        public DateTime DataUpload {  get; set; }
        public Guid UsuarioId { get; set; }
        public Guid EventosId { get; set; }
    }
}
