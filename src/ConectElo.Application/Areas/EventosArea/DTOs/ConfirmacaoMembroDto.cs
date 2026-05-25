using ConectElo.Domain.Areas.Geral.Enuns;

namespace ConectElo.Application.Areas.EventosArea.DTOs
{
    public class ConfirmacaoMembroDto
    {
        public Guid UsuarioId { get; set; }
        public string Nome { get; set; }
        public string? FotoPerfil { get; set; }
        public StatusConfirmacaoEventoEnum Status { get; set; }
    }
}
