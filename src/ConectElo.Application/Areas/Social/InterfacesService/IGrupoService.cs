using ConectElo.Domain.Areas.Social.Entities;

namespace ConectElo.Application.Areas.Social.InterfacesService
{
    public interface IGrupoService
    {
        Task<Grupo> CriarGrupo(Grupo grupo);

        Task ExcluirGrupo(Grupo grupo);

        Task EditarGrupo(Grupo grupo);

        Task<Grupo?> BuscarGrupoPorId(Guid id);
    }
}
