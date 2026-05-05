using ConectElo.API.Areas.Base.Controllers;
using ConectElo.Application.Areas.Social.DTOs.Perfil;
using ConectElo.Application.Areas.Social.InterfacesService;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ConectElo.API.Areas.Social.Controllers
{
    [Route("api/Perfil")]
    [ApiController]
    public class PerfilController : BaseController
    {
        private readonly IUsuarioService _usuarioService;
        private Guid usuarioIdLogado => Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        public PerfilController(IUsuarioService usuarioService, IWebHostEnvironment env) : base(env)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet]
        [Route("ObterPerfil")]
        public async Task<IActionResult> ObterPerfil()
        {
            try
            {
                var resultado = await _usuarioService.ObterPerfilAsync(usuarioIdLogado);
                return OkResponse(resultado);
            }
            catch (Exception ex)
            {
                return BadRequestResponse(ex.Message);
            }
        }

        [HttpPut]
        [Route("AtualizarPerfil")]
        public async Task<IActionResult> AtualizarPerfil([FromBody] AtualizarPerfilDto dto)
        {
            try
            {
                var resultado = await _usuarioService.AtualizarPerfilAsync(usuarioIdLogado, dto);
                return OkResponse(resultado);
            }
            catch (Exception ex)
            {
                return ErrorResponse(ex);
            }
        }

        [HttpPatch("AtualizaFoto")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> AtualizarFoto([FromForm] UploadFotoDto request)
        {
            try
            {
                if (request.Foto is null || request.Foto.Length == 0)
                    return BadRequestResponse("Nenhuma foto enviada.");

                var fotoDto = new AtualizarFotoDto
                {
                    Conteudo = request.Foto.OpenReadStream(),
                    NomeArquivo = request.Foto.FileName,
                    Tamanho = request.Foto.Length
                };

                var resultado = await _usuarioService.AtualizarFotoPerfilAsync(usuarioIdLogado, fotoDto);
                return OkResponse(resultado, "Foto atualizada com sucesso.");
            }
            catch (Exception ex)
            {
                return ErrorResponse(ex);
            }
        }
    }
}
