using ConectElo.API.Areas.Base.Controllers;
using ConectElo.Application.Areas.Social.DTOs;
using ConectElo.Application.Areas.Social.InterfacesService;
using ConectElo.Domain.Areas.Social.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ConectElo.API.Areas.Social.Controllers
{
    [Route("api/membrosGrupo")]
    [ApiController]
    public class membrosGrupoController : BaseController
    {
        private readonly IMembrosGrupoService _membrosGrupoService;

        public membrosGrupoController(IMembrosGrupoService membrosGrupoService, IWebHostEnvironment env) : base(env)
        {
            _membrosGrupoService = membrosGrupoService;
        }

        [HttpPost]
        [Route("Salvar")]
        public async Task<IActionResult> SalvarmembrosGrupo(CriarMembroGrupoDto membroGrupo)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                var resultado = await _membrosGrupoService.CriarMembroGrupo(membroGrupo);
                return CreatedReponse(resultado, "Membro cadastrado com sucesso!");
            }
            catch (Exception err)
            {
                return ErrorResponse(err);
            }
        }

        [HttpGet]
        [Route("Buscar")]
        public async Task<IActionResult> BuscarmembrosGrupoPorId(Guid id)
        {
            try
            {
                var membro = await _membrosGrupoService.BuscarMembroPorId(id);

                if (membro == null)
                    return NotFoundResponse($"Membro com ID {id} não foi encontrado.");

                return OkResponse(membro, "Membro encontrado com sucesso.");
            }
            catch (Exception err)
            {
                return ErrorResponse(err, "Erro ao tentar localizar o membro.");
            }
        }

        [HttpPost]
        [Route("Editar")]
        public async Task<IActionResult> EditarmembrosGrupo(MembrosGrupo membroGrupo)
        {
            try
            {
                if (membroGrupo == null)
                    return BadRequestResponse("Dados inválidos para edição.");

                await _membrosGrupoService.EditarMembroGrupo(membroGrupo);
                return OkResponse(true, "Membro atualizado com sucesso!");
            }
            catch (Exception err)
            {
                return ErrorResponse(err, "Falha ao tentar atualizar membro.");
            }
        }

        [HttpPost]
        [Route("Delete")]
        public IActionResult ExcluirmembrosGrupo(MembrosGrupo membroGrupo)
        {
            try
            {
                if (membroGrupo == null)
                    return BadRequestResponse("Dados inválidos para exclusão.");

                _membrosGrupoService.ExcluirMembroGrupo(membroGrupo);
                return OkResponse(true, "Membro deletado com sucesso!");
            }
            catch (Exception err)
            {
                return ErrorResponse(err, "Falha ao tentar excluir membro.");
            }
        }
    }
}
