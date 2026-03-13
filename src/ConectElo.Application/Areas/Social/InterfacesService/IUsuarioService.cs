using ConectElo.Application.Areas.Social.DTOs;
using ConectElo.Domain.Areas.Social.Entities;
using Microsoft.AspNetCore.Identity;

namespace ConectElo.Application.Areas.Social.InterfacesService
{
    public interface IUsuarioService
    {
        Task<IdentityResult> CriarUsuario(RegistrarUsuarioDto usuario);

        Task ExcluirUsuario(Usuario usuario);

        Task EditarUsuario(Usuario usuario);

        Task<Usuario?> BuscarUsuarioPorId(Guid id);
    }
}
