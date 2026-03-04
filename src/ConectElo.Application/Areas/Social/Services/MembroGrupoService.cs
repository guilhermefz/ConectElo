using AutoMapper;
using ConectElo.Application.Areas.Social.DTOs;
using ConectElo.Application.Areas.Social.InterfacesService;
using ConectElo.Domain.Areas.Social.Entities;
using ConectElo.Domain.Areas.Social.InterfacesRepository;

namespace ConectElo.Application.Areas.Social.Services
{
    public class MembroGrupoService : IMembrosGrupoService
    {
        private readonly IMembrosGrupoRepository _membroGrupoRepository;
        private readonly IGrupoRepository _grupoRepository;
        private readonly IMapper _mapper;

        public MembroGrupoService(IMembrosGrupoRepository membroGrupoRepository, IMapper mapper, IGrupoRepository grupoRepository)
        {
            _membroGrupoRepository = membroGrupoRepository;
            _mapper = mapper;
            _grupoRepository = grupoRepository;
        }

        public async Task<MembrosGrupo?> BuscarMembroPorId(Guid id)
        {
            return await _membroGrupoRepository.SelecionarPorId(id);
        }

        public async Task<CriarMembroGrupoDto> CriarMembroGrupo(CriarMembroGrupoDto membroDto)
        {
            var membro = _mapper.Map<MembrosGrupo>(membroDto);

            await _membroGrupoRepository.Inserir(membro);
            return membroDto;
        }

        public async Task EditarMembroGrupo(MembrosGrupo membro)
        {
            await _membroGrupoRepository.Atualizar(membro);
        }

        public async Task ExcluirMembroGrupo(MembrosGrupo membro)
        {
            await _membroGrupoRepository.Excluir(membro);
        }
    }
}
