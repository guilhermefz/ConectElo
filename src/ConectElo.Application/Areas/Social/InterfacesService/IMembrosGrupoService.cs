using ConectElo.Application.Areas.Social.DTOs;
using ConectElo.Domain.Areas.Social.Entities;

namespace ConectElo.Application.Areas.Social.InterfacesService
{
    public interface IMembrosGrupoService
    {
        Task<CriarMembroGrupoDto> CriarMembroGrupo(CriarMembroGrupoDto membro);
        Task ExcluirMembroGrupo(MembrosGrupo membro);
        Task EditarMembroGrupo(MembrosGrupo membro);
        Task<MembrosGrupo?> BuscarMembroPorId(Guid id);
    }
}
