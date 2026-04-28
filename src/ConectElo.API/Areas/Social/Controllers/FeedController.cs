using ConectElo.API.Areas.Base.Controllers;
using ConectElo.Application.Areas.Social.InterfacesService;
using Microsoft.AspNetCore.Mvc;

namespace ConectElo.API.Areas.Social.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedController : BaseController
    {
        private readonly IFeedService _feedService;

        public FeedController(IFeedService feedService, IWebHostEnvironment env) : base(env)
        {
            _feedService = feedService;
        }

        [HttpGet]
        public async Task<IActionResult> ObterFeed( [FromQuery] Guid usuarioId, [FromQuery] int pagina = 1, [FromQuery] int tamanhoPagina = 20)
        {
            try
            {
                if (usuarioId == Guid.Empty)
                    return BadRequestResponse("Usuário inválido.");

                var feed = await _feedService.ObterFeed(usuarioId, pagina, tamanhoPagina);
                return OkResponse(feed, "Feed carregado com sucesso.");
            }
            catch (Exception ex)
            {
                return ErrorResponse(ex);
            }
        }
    }
}
