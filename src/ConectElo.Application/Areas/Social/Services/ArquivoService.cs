using ConectElo.Application.Areas.Social.DTOs.Perfil;
using ConectElo.Application.Areas.Social.InterfacesService;
using ConectElo.Domain.Areas.Social.InterfacesRepository;
using ConectElo.Domain.Exceptions;

namespace ConectElo.Application.Areas.Social.Services
{
    public class ArquivoService : IArquivoService
    {
        private readonly IArquivoRepository _arquivoRepository;
        private static readonly string[] _extensoesPermitidas = [".jpg", ".jpeg", ".png", ".webp"];
        private const long TamanhoMaximoBytes = 5 * 1024 * 1024;

        public ArquivoService (IArquivoRepository arquivoRepository)
        {
            _arquivoRepository = arquivoRepository;
        }

        public async Task<string> SalvarFotoPerfilAsync(AtualizarFotoDto arquivo, Guid usuarioId)
        {
            if (arquivo.Tamanho > TamanhoMaximoBytes)
                throw new BusinessException("A foto deve ter no máximo 5 MB");

            var extensao = Path.GetExtension(arquivo.NomeArquivo).ToLowerInvariant();
            if (!_extensoesPermitidas.Contains(extensao))
                throw new BusinessException("Formato não permitido. Use JPG, PNG ou WebP.");

            return await _arquivoRepository.SalvarFotoPerfilAsync(
                arquivo.Conteudo,
                arquivo.NomeArquivo,
                arquivo.Tamanho,
                usuarioId
            );
        }

        public async Task<string> SalvarFotoGrupoAsync(AtualizarFotoDto arquivo, Guid grupoId)
        {
            if (arquivo.Tamanho > TamanhoMaximoBytes)
                throw new BusinessException("A foto deve ter no máximo 5 MB");

            var extensao = Path.GetExtension(arquivo.NomeArquivo).ToLowerInvariant();
            if (!_extensoesPermitidas.Contains(extensao))
                throw new BusinessException("Formato não permitido. Use JPG, PNG ou WebP.");

            return await _arquivoRepository.SalvarFotoGrupoAsync(
                arquivo.Conteudo,
                arquivo.NomeArquivo,
                arquivo.Tamanho,
                grupoId
            );
        }

        public void DeletarArquivo(string caminhoRelativo)
        {
           _arquivoRepository.Deletar(caminhoRelativo);
        }
    }
}
