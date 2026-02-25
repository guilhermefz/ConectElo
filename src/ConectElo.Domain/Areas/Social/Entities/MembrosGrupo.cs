using ConectElo.Domain.Areas.Base;
using ConectElo.Domain.Areas.Social.Enuns;

namespace ConectElo.Domain.Areas.Social.Entities
{
    public class MembrosGrupo : EntityBase
    {
        public TipoPermissaoMembroEnum Tipo {  get; set; }
        public DateTime DataEntrada { get; set; }
        public DateTime? DataSaida { get; set; }
        public Guid UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
        public Guid GrupoId { get; set; }
        public Grupo Grupo { get; set; }
    }
}
