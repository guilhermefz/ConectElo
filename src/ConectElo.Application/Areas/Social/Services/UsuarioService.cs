using AutoMapper;
using ConectElo.Application.Areas.Social.DTOs;
using ConectElo.Application.Areas.Social.InterfacesService;
using ConectElo.Domain.Areas.Social.Entities;
using ConectElo.Domain.Areas.Social.InterfacesRepository;
using Microsoft.AspNetCore.Identity;

namespace ConectElo.Application.Areas.Social.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly UserManager<Usuario> _userManager;
        private readonly IMapper _mapper;

        public UsuarioService (IUsuarioRepository usuarioRepository, UserManager<Usuario> userManager, IMapper mapper)
        {
            _usuarioRepository = usuarioRepository;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<IdentityResult> CriarUsuario(RegistrarUsuarioDto usuario)
        {
            var user = _mapper.Map<Usuario>(usuario);

            return await _userManager.CreateAsync(user, user.PasswordHash);
        }

        public async Task ExcluirUsuario(Usuario usuario)
        {
           await _usuarioRepository.Excluir(usuario);
        }

        public async Task EditarUsuario(Usuario usuario)
        {
            await _usuarioRepository.Atualizar(usuario);
        }

        public async Task<Usuario?> BuscarUsuarioPorId(Guid id)
        {
            return await _userManager.FindByIdAsync(id.ToString());
        }
    }
}
