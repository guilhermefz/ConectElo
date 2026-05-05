using AutoMapper;
using ConectElo.Application.Areas.Social.DTOs;
using ConectElo.Application.Areas.Social.InterfacesService;
using ConectElo.Domain.Areas.Social.Entities;
using ConectElo.Domain.Areas.Social.Enuns;
using ConectElo.Domain.Areas.Social.InterfacesRepository;

namespace ConectElo.Application.Areas.Social.Services
{
    public class GrupoService : IGrupoService
    {
        private readonly IGrupoRepository _grupoRepository;
        private readonly IMuralRepository _muralRepository;
        private readonly IMapper _mapper;

        public GrupoService (IGrupoRepository grupoRepository, IMapper mapper, IMuralRepository muralRepository)
        {
            _grupoRepository = grupoRepository;
            _mapper = mapper;
            _muralRepository = muralRepository;
        }

        public async Task<BuscarGrupoDto?> BuscarGrupoPorId(Guid id)
        {
            var grupo = await _grupoRepository.ObterGrupoComInclude(id);
            return _mapper.Map<BuscarGrupoDto>(grupo);

        }

        public async Task<CriarGrupoDto?> CriarGrupo(CriarGrupoDto dto)
        {
            var mural = new Mural();
            await _muralRepository.Inserir(mural);

            var grupo = _mapper.Map<Grupo>(dto);

            grupo.MuralId = mural.Id;
            var agora = DateTime.UtcNow;
            grupo.DataCriacao = agora;
            grupo.UltimaAtualizacao = agora;
            grupo.Membros.Add(new MembrosGrupo
            {
                UsuarioId = dto.ProprietarioId,
                DataEntrada = DateTime.UtcNow,
                Tipo = TipoPermissaoMembroEnum.Proprietario,
            });

            await _grupoRepository.Inserir(grupo);

            var grupoCriado = await _grupoRepository.ObterGrupoComInclude(grupo.Id);
            return _mapper.Map<CriarGrupoDto>(grupoCriado);
        }

        public async Task EditarGrupo(EditarGrupoDto dto)
        {
            var grupoSemEdicao = await _grupoRepository.SelecionarPorId(dto.id);

            if (grupoSemEdicao == null)
                throw new Exception("Grupo não encontrado");


            var grupo = _mapper.Map(dto, grupoSemEdicao);

            await _grupoRepository.Atualizar(grupo);
        }

        public async Task<IEnumerable<GrupoExibicaoDto>> BuscarGruposPorUsuario(Guid usuarioId)
        {
            var grupos = await _grupoRepository.BuscarPorUsuario(usuarioId);
            return _mapper.Map<IEnumerable<GrupoExibicaoDto>>(grupos);
        }

        public async Task ExcluirGrupo(Guid id)
        {
            var grupo = await _grupoRepository.SelecionarPorId(id);
            await _grupoRepository.Excluir(grupo);
        }
    }
}
