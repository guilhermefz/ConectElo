namespace ConectElo.Application.Areas.Social.DTOs.Perfil
{
    public class AtualizarFotoDto
    {
        public Stream Conteudo { get; set; }
        public string NomeArquivo { get; set; }
        public long Tamanho { get; set; }
    }
}
