using ConectElo.API.Areas.Base.Controllers;
using ConectElo.Application.Areas.Home.InterfacesService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConectElo.API.Areas.Home
{
    [Authorize]
    [Route("api/Home")]
    [ApiController]
    public class HomeController : BaseController
    {
        private readonly IHomeService _homeService;

        public HomeController(IWebHostEnvironment env, IHomeService homeService) : base(env)
        {
            _homeService = homeService;
        }
    }
}
