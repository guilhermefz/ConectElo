using ConectElo.Domain.Areas.Social.InterfacesRepository;
using Microsoft.AspNetCore.Hosting;

namespace ConectElo.Infra.Areas.Social.Repositories
{
    public class ArquivoRepository : IArquivoRepository
    {
        private readonly IWebHostEnvironment _env;

        public ArquivoRepository(IWebHostEnvironment env)
        {
            _env = env;
        }

        public void Deletar(string caminhoRelativo)
        {
            var caminhoCompleto = Path.Combine(_env.ContentRootPath, caminhoRelativo.TrimStart('/'));

            if (File.Exists(caminhoCompleto))
                File.Delete(caminhoCompleto);
        }

        public async Task<string> SalvarFotoPerfilAsync(Stream conteudo, string nomeArquivo, long tamanho, Guid usuarioId)
        {
            var extensao = Path.GetExtension(nomeArquivo).ToLowerInvariant();
            var nomeFinal = $"{usuarioId}{extensao}";
            var pasta = Path.Combine(_env.ContentRootPath, "uploads", "fotos-perfil");

            Directory.CreateDirectory(pasta);

            var caminhoCompleto = Path.Combine(pasta, nomeFinal);

            await using var stream = new FileStream(caminhoCompleto, FileMode.Create);
            await conteudo.CopyToAsync(stream);

            return $"/uploads/fotos-perfil/{nomeFinal}";
        }
    }
}
