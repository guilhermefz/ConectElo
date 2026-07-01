using ConectElo.Domain.Areas.Social.Enuns;

namespace ConectElo.Application.Areas.Social.DTOs
{
    public class MembroGrupoExibicaoDto
    {
        public Guid UsuarioId { get; set; }
        public string NomeUsuario { get; set; }
        public string? FotoPerfilUrl { get; set; }
        public DateTime DataEntrada { get; set; }
        public TipoPermissaoMembroEnum Tipo { get; set; }
    }
}
