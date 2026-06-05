namespace ConectElo.Application.Areas.Home.DTOs
{
    public class AtividadeRecenteDto
    {
        public Guid Id { get; set; }
        public string Conteudo { get; set; }
        public string? LinkRul { get; set; }
        public DateTime DataEnvio { get; set; }
    }
}
