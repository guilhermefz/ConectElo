using ConectElo.API.Areas.Base.Controllers;
using ConectElo.Application.Areas.Social.DTOs;
using ConectElo.Application.Areas.Social.InterfacesService;
using ConectElo.Domain.Areas.Social.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ConectElo.API.Areas.Social.Controllers
{
    [Route("api/Usuario")]
    [ApiController]
    public class UsuarioController : BaseController
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService, IWebHostEnvironment env) : base(env)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost]
        [Route("Salvar")]
        public async Task<IActionResult> SalvarUsuario([FromBody] RegistrarUsuarioDto usuario)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                var resultado = await _usuarioService.CriarUsuario(usuario);

                if (resultado.Succeeded)
                    return CreatedReponse(resultado, "Usuario cadastrado com sucesso!");

                var erros = resultado.Errors.Select(e => e.Description).ToList();
                return BadRequestResponse("Falha ao registar utilizador", erros);
            }
            catch (Exception err)
            {
                return ErrorResponse(err);
            }
        }

        [HttpGet]
        [Route("Buscar")]
        public async Task<IActionResult> BuscarUsuarioPorId (Guid id)
        {
            try
            {
                var usuario = await _usuarioService.BuscarUsuarioPorId(id);

                if (usuario == null)
                    return NotFoundResponse($"Usuário com ID {id} não foi encontrado.");

                return OkResponse(usuario, "Usuário encontrado com sucesso.");
            }
            catch (Exception err)
            {
                return ErrorResponse(err, "Erro ao tentar localizar o usuário.");
            }         
        }

        [HttpPost]
        [Route("Editar")]
        public async Task<IActionResult> EditarUsuario(Usuario usuario)
        {
            try
            {
                if(usuario == null)
                    return BadRequestResponse("Dados inválidos para edição.");

                await _usuarioService.EditarUsuario(usuario);
                return OkResponse(true, "Usuário atualizado com sucesso!");
            }
            catch (Exception err)
            {
                return ErrorResponse(err, "Falha ao tentar atualizar usuário.");
            }
        }

        [HttpPost]
        [Route("Delete")]
        public async Task<IActionResult> ExcluirUsuario(Usuario usuario)
        {
            try
            {
                if(usuario == null)
                    return BadRequestResponse("Dados inválidos para exclusão.");

                var resultado = await _usuarioService.ExcluirUsuario(usuario);

                if (resultado.Succeeded)
                    return OkResponse(true, "Usuário deletado com sucesso!");

                var erros = resultado.Errors.Select(e => e.Description).ToList();
                return BadRequestResponse("Falha ao excluir usuário.", erros);
            }
            catch (Exception err)
            {
                return ErrorResponse(err, "Falha ao tentar excluir usuário.");
            }
        }
    }
}
