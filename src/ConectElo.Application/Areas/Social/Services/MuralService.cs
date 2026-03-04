using ConectElo.Application.Areas.Social.InterfacesService;
using ConectElo.Domain.Areas.Social.Entities;
using ConectElo.Domain.Areas.Social.InterfacesRepository;

namespace ConectElo.Application.Areas.Social.Services
{
    public class MuralService : IMuralService
    {
        private readonly IMuralRepository _muralRepository;

        public MuralService(IMuralRepository muralRepository)
        {
            _muralRepository = muralRepository;
        }

        public async Task<Mural?> BuscarMuralPorId(Guid id)
        {
            return await _muralRepository.SelecionarPorId(id);
        }

        public async Task<Mural> CriarMural(Mural mural)
        {
            await _muralRepository.Inserir(mural);
            return mural;
        }

        public async Task EditarMural(Mural mural)
        {
            await _muralRepository.Atualizar(mural);
        }

        public async Task ExcluirMural(Mural mural)
        {
            await _muralRepository.Excluir(mural);
        }
    }
}
