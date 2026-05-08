using ConectElo.API.Areas.Base.Controllers;
using ConectElo.Application.Areas.Social.DTOs;
using ConectElo.Application.Areas.Social.DTOs.Perfil;
using ConectElo.Application.Areas.Social.InterfacesService;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ConectElo.API.Areas.Social.Controllers
{
    [Route("api/Grupo")]
    [ApiController]
    public class GrupoController : BaseController
    {
        private readonly IGrupoService _grupoService;
        private Guid usuarioIdLogado => Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        public GrupoController(IGrupoService grupoService, IWebHostEnvironment env) : base(env)
        {
            _grupoService = grupoService;
        }

        [HttpPost]
        [Route("Salvar")]
        public async Task<IActionResult> SalvarGrupo([FromBody] CriarGrupoDto grupo)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequestResponse("Dados inválidos para requisição.");

                var resultado = await _grupoService.CriarGrupo(grupo);
                return CreatedReponse(resultado, "Grupo criado com sucesso!");
            }
            catch (Exception ex)
            {
                return ErrorResponse(ex, "Erro ao tentar criar grupo.");
            }
        }

        [HttpGet]
        [Route("BuscarPorUsuario/{usuarioId:guid}")]
        public async Task<IActionResult> BuscarGruposPorUsuario(Guid usuarioId)
        {
            try
            {
                var grupos = await _grupoService.BuscarGruposPorUsuario(usuarioId);
                return OkResponse(grupos, "Grupos encontrados com sucesso!");
            }
            catch (Exception ex)
            {
                return ErrorResponse(ex, "Erro ao buscar grupos do usuário.");
            }
        }

        [HttpGet]
        [Route("Buscar")]
        public async Task<IActionResult> BuscarGrupoPorId(Guid id)
        {
            try
            {
                var grupo = await _grupoService.BuscarGrupoPorId(id);

                if (grupo == null)
                    return NotFoundResponse($"Grupo com ID {id} não foi encontrado.");

                return OkResponse(grupo, "Grupo encontrado com sucesso!");
            }
            catch (Exception ex)
            {
                return ErrorResponse(ex, "Não foi possível localizar o grupo");
            }
        }

        [HttpPost]
        [Route("Editar")]
        public async Task<IActionResult> EditarGrupo(EditarGrupoDto grupo)
        {
            try
            {
                if (grupo == null)
                    return BadRequestResponse("Dados inválidos para edição.");

                await _grupoService.EditarGrupo(grupo);
                return OkResponse(true, "Grupo atualizado com sucesso.");
            }
            catch (Exception ex)
            {
                return ErrorResponse(ex, "Falha ao tentar atualizar o grupo.");
            }
        }

        [HttpPost]
        [Route("Excluir")]
        public async Task<IActionResult> ExcluirGrupo(Guid id)
        {
            try
            {
                if (id == null)
                    return BadRequestResponse("Dados inválidos para exclusão.");

                await _grupoService.ExcluirGrupo(id);
                return OkResponse(true, "Grupo deletado com sucesso!");
            }
            catch(Exception ex)
            {
                return ErrorResponse(ex, "Falha ao tentar excluir grupo.");
            }
        }

        [HttpPost("{grupoId:guid}/GerarConvite")]
        public async Task<IActionResult> GerarConvite(Guid grupoId)
        {
            try
            {
                var codigo = await _grupoService.GerarCodigoConviteAsync(grupoId, usuarioIdLogado);
                return OkResponse(codigo, "Link de convite gerado com sucesso.");
            }
            catch (Exception ex)
            {
                return ErrorResponse(ex);
            }
        }

        [HttpPost("EntrarPorConvite/{codigo}")]
        public async Task<IActionResult> EntrarPorConvite(string codigo)
        {
            try
            {
                var grupo = await _grupoService.EntrarPorConviteAsync(codigo, usuarioIdLogado);
                return OkResponse(grupo, "Você entrou no grupo com sucesso.");
            }
            catch (Exception ex)
            {
                return ErrorResponse(ex);
            }
        }

        [HttpPatch("{grupoId:guid}/AtualizarFoto")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> AtualizarFotoGrupo(Guid grupoId, IFormFile foto)
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

                var resultado = await _grupoService.AtualizarFotoGrupoAsync(grupoId, usuarioIdLogado, fotoDto);
                return OkResponse(resultado, "Foto do grupo atualizada com sucesso.");
            }
            catch (Exception ex)
            {
                return ErrorResponse(ex);
            }
        }
    }
}
