using ConectElo.Application.Areas.Social.DTOs.Perfil;

namespace ConectElo.Application.Areas.Social.InterfacesService
{
    public interface IArquivoService
    {
        Task<string> SalvarFotoPerfilAsync(AtualizarFotoDto arquivo, Guid usuarioId);

        void DeletarArquivo(string caminhoRelativo);
    }
}
