using ConectElo.Application.Areas.EventosArea.DTOs;
using ConectElo.Domain.Areas.Geral.Enuns;

namespace ConectElo.Application.Areas.EventosArea.InterfacesService
{
    public interface IConfirmacaoEventoService
    {
        Task Registrar(Guid eventoId, Guid usuarioId, StatusConfirmacaoEventoEnum status);
        Task<ConfirmacoesEventoDto> ListarConfirmacoes(Guid eventoId, Guid usuarioId);
    }
}
