using ConectElo.Application.Areas.Social.DTOs;
using ConectElo.Domain.Areas.Social.Entities;

namespace ConectElo.Application.Areas.Social.InterfacesService
{
    public interface IGrupoService
    {
        Task<CriarGrupoDto?> CriarGrupo(CriarGrupoDto grupo);

        Task ExcluirGrupo(Guid id);

        Task EditarGrupo(EditarGrupoDto grupo);

        Task<BuscarGrupoDto?> BuscarGrupoPorId(Guid id);
    }
}
