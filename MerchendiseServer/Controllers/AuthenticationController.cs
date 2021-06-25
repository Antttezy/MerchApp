using Domain.Core.Models;
using Domain.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MerchendiseServer.Controllers
{
    [ApiController]
    public class AuthenticationController : Controller
    {
        private IAuthenticator<Coordinator> coordinatorAuthenticator;
        private readonly IAuthenticator<Merchendiser> merchendiserAuthenticator;

        public AuthenticationController(IAuthenticator<Coordinator> coordinatorAuthenticator, IAuthenticator<Merchendiser> merchendiserAuthenticator)
        {
            this.coordinatorAuthenticator = coordinatorAuthenticator;
            this.merchendiserAuthenticator = merchendiserAuthenticator;
        }

        [HttpPost]
        [Route("coord/login")]
        public async Task<IActionResult> AuthenticateCoord([FromBody] Authed data)
        {
            return Json(await coordinatorAuthenticator.IsCorrect(data.Login, data.Password));
        }

        [HttpPost]
        [Route("merch/login")]
        public async Task<IActionResult> AuthenticateMerch([FromBody] Authed data)
        {
            return Json(await merchendiserAuthenticator.IsCorrect(data.Login, data.Password));
        }
    }
}
