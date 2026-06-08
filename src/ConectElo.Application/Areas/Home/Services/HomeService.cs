using AutoMapper;
using ConectElo.Application.Areas.Comunicacao.InterfacesService;
using ConectElo.Application.Areas.EventosArea.InterfacesService;
using ConectElo.Application.Areas.Home.DTOs;
using ConectElo.Application.Areas.Home.InterfacesService;
using ConectElo.Application.Areas.Social.InterfacesService;

namespace ConectElo.Application.Areas.Home.Services
{
    public class HomeService : IHomeService
    {
        private readonly IGrupoService _grupoService;
        private readonly IEventoService _eventoService;
        private readonly INotificacaoService _notificacaoService;
        private readonly IMapper _mapper;

        public HomeService( IGrupoService grupoService, IEventoService eventoService, INotificacaoService notificacaoService, IMapper mapper)
        {
            _grupoService = grupoService;
            _eventoService = eventoService;
            _notificacaoService = notificacaoService;
            _mapper = mapper;
        }

        public async Task<TelaInicioDto> BuscarTelaInicial(Guid usuarioId)
        {
            var grupos = (await _grupoService.BuscarGruposPorUsuario(usuarioId)).ToList();
            var eventos = await _eventoService.ListarPorUsuario(usuarioId);
            var notificacoes = await _notificacaoService.ListarPorUsuario(usuarioId);

            var eventosFuturos = eventos
                .Where(e => e.DataInicio.HasValue && e.DataInicio.Value > DateTime.UtcNow)
                .OrderBy(e => e.DataInicio)
                .ToList();

            ProximoEventoDto? proximoEvento = null;
            if (eventosFuturos.Any())
            {
                var proximo = eventosFuturos.First();
                proximoEvento = _mapper.Map<ProximoEventoDto>(proximo);
                proximoEvento.NomeGrupo = grupos.FirstOrDefault(g => g.Id == proximo.GrupoId)?.Nome ?? "";
            }

            return new TelaInicioDto
            {
                ProximoEvento = proximoEvento,
                Contadores = new ContadoresDto
                {
                    EventosFuturos = eventosFuturos.Count,
                    AvisosNaoLidos = notificacoes.Count(n => !n.NotificacaoLida)
                },
                Grupos = _mapper.Map<List<GrupoResumoDto>>(grupos),
                AtividadesRecentes = _mapper.Map<List<AtividadeRecenteDto>>(notificacoes.Take(3).ToList())
            };
        }
    }
}
