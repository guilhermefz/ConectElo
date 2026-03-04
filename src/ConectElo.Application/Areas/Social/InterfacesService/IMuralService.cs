using ConectElo.Domain.Areas.Social.Entities;

namespace ConectElo.Application.Areas.Social.InterfacesService
{
    public interface IMuralService
    {
        Task<Mural> CriarMural(Mural mural);

        Task ExcluirMural(Mural mural);

        Task EditarMural(Mural mural);

        Task<Mural?> BuscarMuralPorId(Guid id);
    }
}
