using ConectElo.API.Areas.Base.Controllers;
using ConectElo.Application.Areas.Comunicacao.InterfacesService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConectElo.API.Areas.Comunicacao.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class NotificacoesController : BaseController
    {
        private readonly INotificacaoService _notificacaoService;

        public NotificacoesController(IWebHostEnvironment env, INotificacaoService notificacaoService) : base(env)
        {
            _notificacaoService = notificacaoService;
        }

        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            try
            {
                var avisos = await _notificacaoService.ListarPorUsuario(UsuarioIdLogado);
                return OkResponse(avisos, "Avisos listados com sucesso.");
            }
            catch (Exception ex)
            {
                return ErrorResponse(ex);
            }
        }

        [HttpPut]
        public async Task<IActionResult> MarcarComoLido(Guid id)
        {
            try
            {
                await _notificacaoService.MarcarComoLida(id, UsuarioIdLogado);
                return OkResponse("Aviso marcado como lido com sucesso.");
            }
            catch (Exception ex)
            {
                return ErrorResponse(ex);
            }
        }

        [HttpPut("marcar-todas")]
        public async Task<IActionResult> MarcarTodasComoLidas()
        {
            try
            {
                await _notificacaoService.MarcarTodasComoLidas(UsuarioIdLogado);
                return OkResponse("Todos os avisos marcados como lidos com sucesso.");
            }
            catch (Exception ex)
            {
                return ErrorResponse(ex);
            }
        }
    }
}
