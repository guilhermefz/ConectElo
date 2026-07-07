using ConectElo.API.Areas.Base.Controllers;
using ConectElo.Application.Areas.Social.DTOs.Perfil;
using ConectElo.Application.Areas.Social.InterfacesService;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

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

        [HttpGet]
        [Route("InteressesDisponiveis")]
        public async Task<IActionResult> InteressesDisponiveis()
        {
            try
            {
                var resultado = await _usuarioService.ListarInteressesDisponiveisAsync();
                return OkResponse(resultado);
            }
            catch (Exception ex)
            {
                return ErrorResponse(ex);
            }
        }

        [HttpPut]
        [Route("AtualizarInteresses")]
        public async Task<IActionResult> AtualizarInteresses([FromBody] AtualizarInteressesDto dto)
        {
            try
            {
                var resultado = await _usuarioService.AtualizarInteressesAsync(usuarioIdLogado, dto);
                return OkResponse(resultado, "Interesses atualizados com sucesso.");
            }
            catch (Exception ex)
            {
                return ErrorResponse(ex);
            }
        }

        [HttpPatch("AtualizaFoto")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> AtualizarFoto(IFormFile foto)
        {
            try
            {
                if (foto is null || foto.Length == 0)
                    return BadRequestResponse("Nenhuma foto enviada.");

                var fotoDto = new AtualizarFotoDto
                {
                    Conteudo = foto.OpenReadStream(),
                    NomeArquivo = foto.FileName,
                    Tamanho = foto.Length
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
