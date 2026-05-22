using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using ConectElo.Domain.Areas.Social.InterfacesRepository;
using Microsoft.AspNetCore.Hosting;

namespace ConectElo.Infra.Areas.Social.Repositories
{
    public class ArquivoRepository : IArquivoRepository
    {
        private readonly Cloudinary _cloudinary;

        public ArquivoRepository(Cloudinary cloudinary)
        {
            _cloudinary = cloudinary;
        }

        public void Deletar(string urlCloudinary)
        {
            var publicId = ExtrairPublicId(urlCloudinary);
            if (publicId is null) return;

            _cloudinary.Destroy(new DeletionParams(publicId));
        }

        public async Task<string> SalvarFotoPerfilAsync(Stream conteudo, string nomeArquivo, long tamanho, Guid usuarioId)
        {
            var updloadParams = new ImageUploadParams
            {
                File = new FileDescription(nomeArquivo, conteudo),
                PublicId = $"fotos-perfil/{usuarioId}",
                Overwrite = true
            };

            var resultado = await _cloudinary.UploadAsync(updloadParams);
            return resultado.SecureUrl.ToString();
        }

        public async Task<string> SalvarFotoGrupoAsync(Stream conteudo, string nomeArquivo, long tamanho, Guid grupoId)
        {
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(nomeArquivo, conteudo),
                PublicId = $"fotos-grupo/{grupoId}",
                Overwrite = true
            };

            var resultado = await _cloudinary.UploadAsync(uploadParams);
            return resultado.SecureUrl.ToString();
        }

        public async Task<string> SalvarFotoCapaEventoASync(Stream conteudo, string nomeArquivo, long tamanho, Guid eventoId)
        {
            var uploadsParams = new ImageUploadParams
            {
                File = new FileDescription(nomeArquivo, conteudo),
                PublicId = $"fotos-capa-evento/{eventoId}",
                Overwrite = true
            };

            var resultado = await _cloudinary.UploadAsync(uploadsParams);
            return resultado.SecureUrl.ToString();
        }

        private static string? ExtrairPublicId(string url)
        {
            // https://res.cloudinary.com/{cloud}/image/upload/v{version}/{public_id}.{ext}
            const string marcador = "/upload";
            var indice = url.IndexOf(marcador, StringComparison.Ordinal);
            if (indice < 0) return null;

            var parte = url[(indice + marcador.Length)..];

            // remove prefixo de versão (v1234567890/)
            var primeirasBarra = parte.IndexOf('/');
            if (primeirasBarra > 0 && parte[0] == 'v' && parte[1..primeirasBarra].All(char.IsDigit))
                parte = parte[(primeirasBarra + 1)..];

            // remove extensão
            var ponto = parte.LastIndexOf('.');
            return ponto >= 0 ? parte[..ponto] : parte;
        }
    }
}
