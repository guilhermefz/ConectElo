using ConectElo.Application.Areas.Base;
using Microsoft.AspNetCore.Mvc;

namespace ConectElo.API.Areas.Base.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected readonly IWebHostEnvironment _env;

        public BaseController(IWebHostEnvironment env)
        {
            _env = env;
        }

        protected IActionResult OkResponse<T>(T data, string message = "Operação realizada com sucesso")
        {
            return Ok(BaseResponse<T>.Ok(data, message));
        }

        protected IActionResult CreatedReponse<T>(T data, string message = "Criado com sucesso!")
        {
            return StatusCode(201, BaseResponse<T>.Ok(data, message));
        }

        protected IActionResult BadRequestResponse(string message, List<string>? errors = null)
        {
            return BadRequest(BaseResponse<object>.Falha(message, errors));
        }

        protected IActionResult NotFoundResponse(string message)
        {
            return NotFound(BaseResponse<object>.Falha(message));
        }

        protected IActionResult ErrorResponse(Exception ex, string message = "Erro interno no servidor")
        {
            var listaDeErros = new List<string>();

            if (_env.IsDevelopment())
            {
                listaDeErros.Add(ex.Message);

                if (ex.InnerException != null)
                    listaDeErros.Add(ex.InnerException.Message);
            }

            return StatusCode(500, BaseResponse<object>.Falha(message, listaDeErros));
        }

    }
}
