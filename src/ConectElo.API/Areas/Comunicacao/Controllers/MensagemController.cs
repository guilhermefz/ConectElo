using ConectElo.API.Areas.Base.Controllers;
using ConectElo.Application.Areas.Comunicacao.InterfacesService;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ConectElo.API.Areas.Comunicacao.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MensagemController : BaseController
    {
        private readonly IMensagemService _mensagemService;
        private Guid UsuarioIdLogado => Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        public MensagemController(IMensagemService mensagemService, IWebHostEnvironment env) : base(env)
        {
            _mensagemService = mensagemService;
        }

        [HttpGet("{grupoId}/historico")]
        public async Task<IActionResult> ObterHistorico(Guid grupoId)
        {
            try
            {
                var mensagens = await _mensagemService.ObterHistoricoAsync(grupoId, UsuarioIdLogado);
                return OkResponse(mensagens);
            }
            catch (Exception ex)
            {
                return ErrorResponse(ex);
            }
        }
    }
}
