using ConectElo.API.Areas.Base.Controllers;
using ConectElo.Application.Areas.Social.InterfacesService;
using ConectElo.Domain.Areas.Social.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConectElo.API.Areas.Social.Controllers
{
    [Route("api/Mural")]
    [ApiController]
    public class MuralController : BaseController
    {
        private readonly IMuralService _muralService;

        public MuralController(IMuralService muralService, IWebHostEnvironment env) : base(env)
        {
            _muralService = muralService;
        }

        [HttpPost]
        [Route("Salvar")]
        public async Task<IActionResult> SalvarMural([FromBody] Mural mural)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                var resultado = await _muralService.CriarMural(mural);
                return CreatedReponse(resultado, "Mural cadastrado com sucesso!");
            }
            catch (Exception err)
            {
                return ErrorResponse(err);
            }
        }

        [HttpGet]
        [Route("Buscar")]
        public async Task<IActionResult> BuscarMuralPorId(Guid id)
        {
            try
            {
                var mural = await _muralService.BuscarMuralPorId(id);

                if (mural == null)
                    return NotFoundResponse($"Mural com ID {id} não foi encontrado.");

                return OkResponse(mural, "Mural encontrado com sucesso.");
            }
            catch (Exception err)
            {
                return ErrorResponse(err, "Erro ao tentar localizar o mural.");
            }
        }

        [HttpPost]
        [Route("Editar")]
        public async Task<IActionResult> EditarMural(Mural mural)
        {
            try
            {
                if (mural == null)
                    return BadRequestResponse("Dados inválidos para edição.");

                await _muralService.EditarMural(mural);
                return OkResponse(true, "Mural atualizado com sucesso!");
            }
            catch (Exception err)
            {
                return ErrorResponse(err, "Falha ao tentar atualizar mural.");
            }
        }

        [HttpPost]
        [Route("Delete")]
        public IActionResult ExcluirMural(Mural mural)
        {
            try
            {
                if (mural == null)
                    return BadRequestResponse("Dados inválidos para exclusão.");

                _muralService.ExcluirMural(mural);
                return OkResponse(true, "Mural deletado com sucesso!");
            }
            catch (Exception err)
            {
                return ErrorResponse(err, "Falha ao tentar excluir mural.");
            }
        }
    }
}
