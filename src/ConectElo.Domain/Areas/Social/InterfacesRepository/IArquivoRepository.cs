namespace ConectElo.Domain.Areas.Social.InterfacesRepository
{
    public interface IArquivoRepository
    {
        Task<string> SalvarFotoPerfilAsync(Stream conteudo, string nomeArquivo, long tamanho, Guid usuarioId);
        void Deletar(string caminhoRelativo);
    }
}
