using ConectElo.API.Areas.Base.Controllers;
using ConectElo.Application.Areas.Base;
using ConectElo.Application.Areas.Social.InterfacesService;
using ConectElo.Domain.Areas.Social.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ConectElo.API.Areas.Social.Controllers
{
    [Route("api/[controller]")]
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
        public async Task<IActionResult> SalvarUsuario([FromBody] Usuario usuario)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                var resultado = await _usuarioService.CriarUsuario(usuario);
                return CreatedReponse(resultado, "Usuario cadastrado com sucesso!");
            }
            catch (Exception err)
            {
                return ErrorReponse(err);
            }
        }

        [HttpGet]
        [Route("Buscar")]
        public async Task<IActionResult> BuscarUsuarioPorId (Guid id)
        {
            if(id == null)
                return NotFound();

            var usuario = await _usuarioService.BuscarUsuarioPorId(id);
            return Ok(usuario);
        }

        [HttpPost]
        [Route("Editar")]
        public IActionResult EditarUsuario(Usuario usuario)
        {
            if(usuario == null) 
                return NotFound();

            _usuarioService.EditarUsuario(usuario);
            return Ok();
        }

        [HttpPost]
        [Route("Delete")]
        public IActionResult ExcluirUsuario(Usuario usuario)
        {
            if(usuario == null)
                return NotFound();

            _usuarioService.ExcluirUsuario(usuario);
            return Ok();
        }
    }
}
