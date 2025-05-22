using Application.Commands;
using Application.Services;
using Domain.Security;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Controllers.Base;

namespace WebAPI.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AuthController : FakeLibraryBaseController
    {
        private readonly IAuthManager _authManager;

        public AuthController(IAuthManager authManager)
        {
            _authManager = authManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterClientCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginCommand command)
        {
            if (await _authManager.Authenticate(new ClientCredentials
            {
                ClientId = command.ClientId,
                ClientSecret = command.ClientSecret,
            }))
            {
                return Ok(await Mediator.Send(command));
            }
            else return Unauthorized();
        }
    }
}
