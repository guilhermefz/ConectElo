using ConectElo.Application.Areas.Social.DTOs;

namespace ConectElo.Application.Areas.Social.InterfacesService
{
    public interface IPostagemService
    {
        Task<ExibirPostagemDto> BuscarPostagemPorId(Guid id);
        Task ExcluirPostagem(Guid id);
        Task<CriarPostagemDto> CriarPostagens(CriarPostagemDto dto);
        Task<EditarPostagemDto> EditarPostagem(EditarPostagemDto dto);
    }
}
