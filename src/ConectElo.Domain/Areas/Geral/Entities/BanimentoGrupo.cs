using ConectElo.Domain.Areas.Base;
using ConectElo.Domain.Areas.Social.Entities;
using System.Globalization;

namespace ConectElo.Domain.Areas.Geral.Entities
{
    public class BanimentoGrupo : EntityBase
    {
        public Guid BanidoPor {  get; set; }
        public DateTime DataBanimento { get; set; }
        public string Motivo { get; set; }
        public Guid UsuarioId { get; set; }
        public Usuario? UsuarioBanido { get; set; }
        public Guid GrupoId { get; set; }
        public Grupo? Grupo { get; set; }
    }
}
