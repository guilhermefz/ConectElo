using ConectElo.Domain.Areas.Social.Entities;

namespace ConectElo.Application.Areas.Social.InterfacesService
{
    public interface IUsuarioService
    {
        Task<Usuario> CriarUsuario(Usuario usuario);

        Task ExcluirUsuario(Usuario usuario);

        Task EditarUsuario(Usuario usuario);

        Task<Usuario?> BuscarUsuarioPorId(Guid id);
    }
}
