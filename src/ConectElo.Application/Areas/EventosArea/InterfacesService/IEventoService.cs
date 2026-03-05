using ConectElo.Application.Areas.EventosArea.DTOs;

namespace ConectElo.Application.Areas.EventosArea.InterfacesService
{
    public interface IEventoService
    {
        Task<ExibirEventoDto> BuscarEventoPorId(Guid id);
        Task<CriarEventoDto> CriarEvento(CriarEventoDto dto);
        Task ExcluirEvento (Guid id);
        Task<EditarEventoDto> EditarEvento(EditarEventoDto dto);
    }
}
