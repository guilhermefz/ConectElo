namespace ConectElo.Application.Areas.Comunicacao.DTOs
{
    public class MensagemDto
    {
        public Guid Id { get; set; }
        public string Conteudo { get; set; }
        public string NomeAutor { get; set; }
        public Guid UsuarioId { get; set; }
        public DateTime HorarioEnvio { get; set; }
    }
}
