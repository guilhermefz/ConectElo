namespace ConectElo.Domain.Areas.Social.InterfacesRepository
{
    public interface IArquivoRepository
    {
        Task<string> SalvarFotoPerfilAsync(Stream conteudo, string nomeArquivo, long tamanho, Guid usuarioId);
        Task<string> SalvarFotoGrupoAsync(Stream conteudo, string nomeArquivo, long tamanho, Guid grupoId);
        Task<string> SalvarFotoCapaEventoASync(Stream conteudo, string nomeArquivo, long tamanho, Guid eventoId);
        void Deletar(string caminhoRelativo);
    }
}
