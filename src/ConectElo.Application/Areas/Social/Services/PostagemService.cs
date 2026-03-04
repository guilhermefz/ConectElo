using AutoMapper;
using ConectElo.Application.Areas.Social.DTOs;
using ConectElo.Application.Areas.Social.InterfacesService;
using ConectElo.Domain.Areas.Social.Entities;
using ConectElo.Domain.Areas.Social.InterfacesRepository;

namespace ConectElo.Application.Areas.Social.Services
{
    public class PostagemService : IPostagemService
    {
        private readonly IPostagemRepository _postagemRepository;
        private readonly IMapper _mapper;

        public PostagemService(IPostagemRepository postagemRepository, IMapper mapper)
        {
            _postagemRepository = postagemRepository;
            _mapper = mapper;
        }

        public async Task<ExibirPostagemDto> BuscarPostagemPorId(Guid id)
        {
            var post = await _postagemRepository.SelecionarPorId(id);
            return  _mapper.Map<ExibirPostagemDto>(post);
        }

        public async Task<CriarPostagemDto> CriarPostagens(CriarPostagemDto dto)
        {
            var post = _mapper.Map<Postagens>(dto);
            await _postagemRepository.Inserir(post);
            return dto;
        }

        public async Task<EditarPostagemDto> EditarPostagem(EditarPostagemDto dto)
        {
            var postagem = await _postagemRepository.SelecionarPorId(dto.Id);

            if (postagem is null)
                return null;

            _mapper.Map(dto, postagem);

            await _postagemRepository.Atualizar(postagem);
            return dto;
        }

        public async Task ExcluirPostagem(Guid id)
        {
            var post = await _postagemRepository.SelecionarPorId(id);

            if (post == null) return;

            await _postagemRepository.Excluir(post);
        }
    }
}
