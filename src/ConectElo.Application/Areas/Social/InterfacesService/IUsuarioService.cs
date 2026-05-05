using ConectElo.Application.Areas.Social.DTOs;
using ConectElo.Application.Areas.Social.DTOs.Perfil;
using ConectElo.Domain.Areas.Social.Entities;
using Microsoft.AspNetCore.Identity;

namespace ConectElo.Application.Areas.Social.InterfacesService
{
    public interface IUsuarioService
    {
        Task<IdentityResult> CriarUsuario(RegistrarUsuarioDto usuario);

        Task<IdentityResult> ExcluirUsuario(Usuario usuario);

        Task<IdentityResult> EditarUsuario(EditarUsuarioDto dto);

        Task<Usuario?> BuscarUsuarioPorId(Guid id);

        Task<PerfilUsuarioDto> ObterPerfilAsync(Guid usuarioId);

        Task<PerfilUsuarioDto> AtualizarPerfilAsync(Guid usuarioId, AtualizarPerfilDto dto);

        Task<string> AtualizarFotoPerfilAsync(Guid usuarioId, AtualizarFotoDto foto);
    }
}
