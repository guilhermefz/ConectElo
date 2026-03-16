using ConectElo.Application.Areas.Autenticacao.DTOs;

namespace ConectElo.Application.Areas.Autenticacao.InterfacesService
{
    public interface IAutenticacaoService
    {
        Task<LoginResponseDto> Login(LoginDto dto);
    }
}
