using AutoMapper;
using ConectElo.Application.Areas.Social.DTOs;
using ConectElo.Application.Areas.Social.InterfacesService;
using ConectElo.Domain.Areas.Social.InterfacesRepository;

namespace ConectElo.Application.Areas.Social.Services
{
    public class FeedService : IFeedService
    {
        private readonly IPostagemRepository _postagemRepository;
        private readonly IMapper _mapper;

        public FeedService(IPostagemRepository postagemRepository, IMapper mapper)
        {
            _postagemRepository = postagemRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<FeedPostagemDto>> ObterFeed( Guid usuarioId, int pagina, int tamanhoPagina = 20)
        {
            var postagens = await _postagemRepository.ObterFeedDoUsuario(usuarioId, pagina, tamanhoPagina);

            return _mapper.Map<IEnumerable<FeedPostagemDto>>(postagens);
        }
    }
}
