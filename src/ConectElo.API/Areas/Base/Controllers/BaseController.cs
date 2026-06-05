using ConectElo.Application.Areas.Base;
using ConectElo.Domain.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ConectElo.API.Areas.Base.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected readonly IWebHostEnvironment _env;
        protected Guid UsuarioIdLogado => Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        protected string NomeUsuarioLogado => User.FindFirstValue(JwtRegisteredClaimNames.Name) ?? string.Empty;

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

        protected IActionResult ConflictResponse(string message)
        {
            return StatusCode(409, BaseResponse<object>.Falha(message));
        }

        protected IActionResult UnauthorizedResponse(string message)
        {
            return StatusCode(401, BaseResponse<object>.Falha(message));
        }

        protected IActionResult NotFoundResponse(string message)
        {
            return NotFound(BaseResponse<object>.Falha(message));
        }

        protected IActionResult ErrorResponse(Exception ex, string message = "Erro interno no servidor")
        {
            if (ex is NotFoundException)
                return NotFoundResponse(ex.Message);

            if (ex is BusinessException)
                return BadRequestResponse(ex.Message);

            if (ex is UnathorizedException)
                return UnauthorizedResponse(ex.Message);

            if (ex is ConflictException)
                return ConflictResponse(ex.Message);

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
