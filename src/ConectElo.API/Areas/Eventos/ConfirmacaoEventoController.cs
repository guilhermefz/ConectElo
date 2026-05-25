using ConectElo.API.Areas.Base.Controllers;
using ConectElo.Application.Areas.EventosArea.DTOs;
using ConectElo.Application.Areas.EventosArea.InterfacesService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ConectElo.API.Areas.Eventos
{
    [Authorize]
    [Route("api/Eventos/{eventoId}/Confirmacoes")]
    [ApiController]
    public class ConfirmacaoEventoController : BaseController
    {
        private readonly IConfirmacaoEventoService _confirmacaoEventoService;

        public ConfirmacaoEventoController(IConfirmacaoEventoService confirmacaoEventoService, IWebHostEnvironment env) : base(env)
        {
            _confirmacaoEventoService = confirmacaoEventoService;
        }

        [HttpGet]
        public async Task<IActionResult> Listar(Guid eventoId)
        {
            try
            {
                var usuarioId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
                var resultado = await _confirmacaoEventoService.ListarConfirmacoes(eventoId, usuarioId);
                return OkResponse(resultado, "Confirmações listadas com sucesso.");
            }
            catch (Exception ex) { return ErrorResponse(ex); }
        }

        [HttpPost]
        public async Task<IActionResult> Registrar(Guid eventoId, RegistrarParticipacaoDto dto)
        {
            try
            {
                var usuarioId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
                await _confirmacaoEventoService.Registrar(eventoId, usuarioId, dto.Status);
                return OkResponse("Participação registrada com sucesso.");
            }
            catch (Exception ex) { return ErrorResponse(ex); }
        }
    }
}
