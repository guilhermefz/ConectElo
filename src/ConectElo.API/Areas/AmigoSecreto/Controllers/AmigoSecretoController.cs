using ConectElo.API.Areas.AmigoSecreto.Hubs;
using ConectElo.API.Areas.Base.Controllers;
using ConectElo.Application.Areas.AmigoSecreto.DTOs;
using ConectElo.Application.Areas.AmigoSecreto.InterfacesService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace ConectElo.API.Areas.AmigoSecreto.Controllers
{
    [Authorize]
    [Route("api/AmigoSecreto")]
    [ApiController]
    public class AmigoSecretoController : BaseController
    {
        private readonly IAmigoSecretoService _amigoSecretoService;
        private readonly IHubContext<AmigoSecretoHub> _hubContext;

        public AmigoSecretoController(IWebHostEnvironment env, IAmigoSecretoService amigoSecretoService, IHubContext<AmigoSecretoHub> hubContext) : base(env)
        {
            _amigoSecretoService = amigoSecretoService;
            _hubContext = hubContext;
        }

        [HttpPost("{eventoId}/Agendar")]
        public async Task<IActionResult> AgendarSorteio(Guid eventoId, [FromBody] AgendarSorteioDto dto)
        {
            try
            {
                dto.EventoId = eventoId;
                var jobId = await _amigoSecretoService.AgendarSorteio(dto, UsuarioIdLogado);
                return OkResponse(new { jobId }, "Sorteio agendado com sucesso.");
            }
            catch (Exception ex)
            {
                return ErrorResponse(ex);
            }
        }

        [HttpPost("{eventoId}/SortearAgora")]
        public async Task<IActionResult> SortearAgora(Guid eventoId)
        {
            try
            {
                var resultado = await _amigoSecretoService.SortearAgora(eventoId, UsuarioIdLogado);

                foreach (var participanteId in resultado.ParticipantesIds)
                {
                    await _hubContext.Clients
                        .User(participanteId.ToString())
                        .SendAsync("SorteioRealizado", new
                        {
                            resultado.EventoId,
                            resultado.DataExecucao
                        });
                }

                return OkResponse(resultado, "Sorteio realizado com sucesso.");
            }
            catch (Exception ex)
            {
                return ErrorResponse(ex);
            }
        }

        [HttpPut("{eventoId}/AlterarData")]
        public async Task<IActionResult> AlterarDataSorteio(
            Guid eventoId, [FromBody] AlterarDataSorteioDto dto)
        {
            try
            {
                var jobId = await _amigoSecretoService
                    .AlterarDataSorteio(eventoId, dto.NovaData, UsuarioIdLogado);

                return OkResponse(new { jobId }, "Data do sorteio alterada com sucesso.");
            }
            catch (Exception ex)
            {
                return ErrorResponse(ex);
            }
        }

        [HttpGet("{eventoId}/MeuResultado")]
        public async Task<IActionResult> BuscarMeuResultado(Guid eventoId)
        {
            try
            {
                var resultado = await _amigoSecretoService
                    .BuscarMeuResultado(eventoId, UsuarioIdLogado);

                return OkResponse(resultado, "Resultado buscado com sucesso.");
            }
            catch (Exception ex)
            {
                return ErrorResponse(ex);
            }
        }

        [HttpGet("Chat/{resultadoSorteioId}/Historico")]
        public async Task<IActionResult> BuscarHistorico(Guid resultadoSorteioId)
        {
            try
            {
                var historico = await _amigoSecretoService
                    .BuscarHistorico(resultadoSorteioId, UsuarioIdLogado);

                return OkResponse(historico, "Histórico buscado com sucesso.");
            }
            catch (Exception ex)
            {
                return ErrorResponse(ex);
            }
        }
    }
}
