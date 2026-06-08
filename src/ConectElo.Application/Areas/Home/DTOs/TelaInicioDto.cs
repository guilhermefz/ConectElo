namespace ConectElo.Application.Areas.Home.DTOs
{
    public class TelaInicioDto
    {
        public ProximoEventoDto? ProximoEvento { get; set; }
        public ContadoresDto Contadores { get; set; }
        public List<AtividadeRecenteDto> AtividadesRecentes { get; set; }
        public List<GrupoResumoDto> Grupos { get; set; }
    }
}
