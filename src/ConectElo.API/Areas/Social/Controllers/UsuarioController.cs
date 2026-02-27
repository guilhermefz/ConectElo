using ConectElo.Application.Areas.Social.InterfacesService;
using ConectElo.Domain.Areas.Social.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ConectElo.API.Areas.Social.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost]
        [Route("Salvar")]
        public IActionResult SalvarUsuario(Usuario usuario)
        {
            if (usuario == null)
                return NotFound();

            _usuarioService.CriarUsuario(usuario);
            return Ok();
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
