using ConectElo.API.Areas.Base.Controllers;
using ConectElo.Application.Areas.Autenticacao.DTOs;
using ConectElo.Application.Areas.Autenticacao.InterfacesService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ConectElo.API.Areas.Authorization.Controllers
{
    [AllowAnonymous]
    [Route("api/Autenticacao")]
    [ApiController]
    public class AuthController : BaseController
    {
        private readonly IAutenticacaoService _autenticacaoService;

        public AuthController(IWebHostEnvironment env, IAutenticacaoService autenticacaoService) : base(env) 
        {
            _autenticacaoService = autenticacaoService;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            try
            {
                var response = await _autenticacaoService.Login(dto);
                return OkResponse(response, "Login realizado com sucesso.");
            }
            catch (Exception err)
            {
                return ErrorResponse(err, "Falha ao realizar login.");
            }
        }
    }
}
