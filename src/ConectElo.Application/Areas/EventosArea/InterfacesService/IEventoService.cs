using ConectElo.Application.Areas.EventosArea.DTOs;
using ConectElo.Application.Areas.Social.DTOs.EventosDTO;

namespace ConectElo.Application.Areas.EventosArea.InterfacesService
{
    public interface IEventoService
    {
        Task<ExibirEventoDto> BuscarEventoPorId(Guid id);
        Task<CriarEventoDto> CriarEvento(CriarEventoDto dto);
        Task ExcluirEvento (Guid id);
        Task<EditarEventoDto> EditarEvento(EditarEventoDto dto);
        Task<List<ExibirEventoDto>> ListarPorGrupo(Guid grupoId);
        Task<ExibirAniversarioDto> CriarAniversario(CriarAniversarioDto dto, Guid criadorId);
        Task<ExibirAmigoSecretoDto> CriarAmigoSecreto(CriarAmigoSecretoDto dto, Guid criadorId);
        Task<List<ExibirEventoDto>> ListarPorUsuario(Guid usuarioId);
    }
}
