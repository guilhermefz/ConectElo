using ConectElo.Application.Areas.Social.DTOs.Perfil;

namespace ConectElo.Application.Areas.Social.InterfacesService
{
    public interface IArquivoService
    {
        Task<string> SalvarFotoPerfilAsync(AtualizarFotoDto arquivo, Guid usuarioId);
        Task<string> SalvarFotoGrupoAsync(AtualizarFotoDto arquivo, Guid grupoId);
        void DeletarArquivo(string caminhoRelativo);
    }
}
