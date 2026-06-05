using ConectElo.Application.Areas.Home.DTOs;
using ConectElo.Application.Areas.Home.InterfacesService;

namespace ConectElo.Application.Areas.Home.Services
{
    public class HomeService : IHomeService
    {
        public Task<TelaInicioDto> BuscarTelaInicial(Guid usuarioId)
        {
            throw new NotImplementedException();
        }
    }
}
