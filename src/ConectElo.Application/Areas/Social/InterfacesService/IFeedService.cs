using ConectElo.Application.Areas.Social.DTOs;

namespace ConectElo.Application.Areas.Social.InterfacesService
{
    public interface IFeedService
    {
        Task<IEnumerable<FeedPostagemDto>> ObterFeed( Guid usuarioId, int pagina, int tamanhoPagina);
    }
}
