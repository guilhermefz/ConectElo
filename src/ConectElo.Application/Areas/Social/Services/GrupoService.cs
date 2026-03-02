using ConectElo.Application.Areas.Social.InterfacesService;
using ConectElo.Domain.Areas.Social.Entities;
using ConectElo.Domain.Areas.Social.InterfacesRepository;

namespace ConectElo.Application.Areas.Social.Services
{
    public class GrupoService : IGrupoService
    {
        private readonly IGrupoRepository _grupoRepository;

        public GrupoService (IGrupoRepository grupoRepository)
        {
            _grupoRepository = grupoRepository;
        }

        public async Task<Grupo?> BuscarGrupoPorId(Guid id)
        {
            return await _grupoRepository.SelecionarPorId(id);
        }

        public async Task<Grupo?> CriarGrupo(Grupo grupo)
        {
            await _grupoRepository.Inserir(grupo);
            return grupo;
        }

        public async Task EditarGrupo(Grupo grupo)
        {
            await _grupoRepository.Atualizar(grupo);
        }

        public async Task ExcluirGrupo(Grupo grupo)
        {
            await _grupoRepository.Excluir(grupo);
        }
    }
}
