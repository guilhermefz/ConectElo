using ConectElo.API.Areas.Base.Controllers;
using ConectElo.Application.Areas.Social.DTOs;
using ConectElo.Application.Areas.Social.InterfacesService;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ConectElo.API.Areas.Social.Controllers
{
    [Route("api/Postagem")]
    [ApiController]
    public class PostagemController : BaseController
    {
        private readonly IPostagemService _postagemService;

        public PostagemController(IPostagemService postagemService, IWebHostEnvironment env) : base(env)
        {
            _postagemService = postagemService;
        }

        [HttpPost]
        [Route("Salvar")]
        public async Task<IActionResult> SalvarUsuario([FromBody] CriarPostagemDto postagem)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                var resultado = await _postagemService.CriarPostagens(postagem);
                return CreatedReponse(resultado, "Postagem publicada com sucesso!");
            }
            catch (Exception err)
            {
                return ErrorResponse(err);
            }
        }

        [HttpGet]
        [Route("Buscar")]
        public async Task<IActionResult> BuscarPostPorId(Guid id)
        {
            try
            {
                var postagem = await _postagemService.BuscarPostagemPorId(id);

                if (postagem == null)
                    return NotFoundResponse($"Post com ID {id} não foi encontrado.");

                return OkResponse(postagem, "Post encontrado com sucesso.");
            }
            catch (Exception err)
            {
                return ErrorResponse(err, "Erro ao tentar localizar o Post.");
            }
        }

        [HttpPost]
        [Route("Editar")]
        public async Task<IActionResult> EditarPostagem(EditarPostagemDto postagem)
        {
            try
            {
                if (postagem == null)
                    return BadRequestResponse("Dados inválidos para edição.");

                await _postagemService.EditarPostagem(postagem);
                return OkResponse(true, "Post atualizado com sucesso!");
            }
            catch (Exception err)
            {
                return ErrorResponse(err, "Falha ao tentar atualizar Post.");
            }
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<IActionResult> ExcluirPostagem(Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                    return BadRequestResponse("Dados inválidos para exclusão.");

                await _postagemService.ExcluirPostagem(id);
                return OkResponse(true, "Post deletado com sucesso!");
            }
            catch (Exception err)
            {
                return ErrorResponse(err, "Falha ao tentar excluir Post.");
            }
        }
    }
}
