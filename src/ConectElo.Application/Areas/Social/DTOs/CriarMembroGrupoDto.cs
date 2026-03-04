using ConectElo.Domain.Areas.Social.Entities;
using ConectElo.Domain.Areas.Social.Enuns;

namespace ConectElo.Application.Areas.Social.DTOs
{
    public class CriarMembroGrupoDto
    {
        public TipoPermissaoMembroEnum Tipo { get; set; }
        public DateTime DataEntrada { get; set; }
        public DateTime? DataSaida { get; set; }
        public Guid UsuarioId { get; set; }
        public Guid GrupoId { get; set; }
    }
}
