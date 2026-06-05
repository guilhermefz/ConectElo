using ConectElo.Application.Areas.Home.DTOs;

namespace ConectElo.Application.Areas.Home.InterfacesService
{
    public interface IHomeService
    {
        Task<TelaInicioDto> BuscarTelaInicial (Guid usuarioId);
    }
}
