using ConectElo.Domain.Areas.Base;
using ConectElo.Domain.Areas.Comunicacao.Enuns;
using ConectElo.Domain.Areas.Social.Entities;

namespace ConectElo.Domain.Areas.Comunicacao.Entities
{
    public class Mensagem : EntityBase
    {
        public string Conteudo { get; set; }
        public DateTime HorarioEnvio { get; set; }
        public bool Anonima {  get; set; }
        public TipoMidiaMensagem TipoMidia { get; set;}
        public string? MidiaUrl { get; set; }
        public Guid GrupoId { get; set; }
        public Guid UsuarioId { get; set; }
        public virtual Usuario Autor {  get; set; }
    }
}
