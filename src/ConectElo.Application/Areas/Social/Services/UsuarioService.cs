using AutoMapper;
using ConectElo.Application.Areas.Social.DTOs;
using ConectElo.Application.Areas.Social.InterfacesService;
using ConectElo.Domain.Areas.Social.Entities;
using ConectElo.Domain.Exceptions;
using Microsoft.AspNetCore.Identity;

namespace ConectElo.Application.Areas.Social.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly IMapper _mapper;

        public UsuarioService(UserManager<Usuario> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<IdentityResult> CriarUsuario(RegistrarUsuarioDto usuario)
        {
            var user = _mapper.Map<Usuario>(usuario);
            return await _userManager.CreateAsync(user, usuario.password);
        }

        public async Task<IdentityResult> ExcluirUsuario(Usuario usuario)
        {
            return await _userManager.DeleteAsync(usuario);
        }

        public async Task<IdentityResult> EditarUsuario(EditarUsuarioDto dto)
        {
            var usuario = await _userManager.FindByIdAsync(dto.Id.ToString())
                ?? throw new NotFoundException($"Usuário com ID {dto.Id} não foi encontrado.");

            _mapper.Map(dto, usuario);

            return await _userManager.UpdateAsync(usuario);
        }

        public async Task<Usuario?> BuscarUsuarioPorId(Guid id)
        {
            return await _userManager.FindByIdAsync(id.ToString());
        }
    }
}
