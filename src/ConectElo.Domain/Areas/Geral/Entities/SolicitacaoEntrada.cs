using ConectElo.Domain.Areas.Base;
using ConectElo.Domain.Areas.Geral.Enuns;
using ConectElo.Domain.Areas.Social.Entities;

namespace ConectElo.Domain.Areas.Geral.Entities
{
    public class SolicitacaoEntrada : EntityBase
    {
        public StatusSolicitacaoEntradaEnum Status {  get; set; }
        public DateTime DataSolicitacao { get; set; }
        public Guid GrupoId { get; set; }
        public Grupo? Grupo { get; set; }
        public Guid UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }
    }
}
