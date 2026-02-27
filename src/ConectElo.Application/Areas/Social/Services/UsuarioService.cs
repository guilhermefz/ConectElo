using ConectElo.Application.Areas.Social.InterfacesService;
using ConectElo.Domain.Areas.Social.Entities;
using ConectElo.Domain.Areas.Social.InterfacesRepository;

namespace ConectElo.Application.Areas.Social.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService (IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task CriarUsuario(Usuario usuario)
        {
            await _usuarioRepository.Inserir(usuario);
        }

        public async Task ExcluirUsuario(Usuario usuario)
        {
           await _usuarioRepository.Excluir(usuario);
        }

        public async Task EditarUsuario(Usuario usuario)
        {
            await _usuarioRepository.Atualizar(usuario);
        }

        public async Task<Usuario> BuscarUsuarioPorId(Guid id)
        {
            return await _usuarioRepository.SelecionarPorId(id);
        }
    }
}
