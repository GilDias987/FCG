using FCG.ApplicationCore.Feature.Usuario.Query.LoginUsuario;
using FCG.WebAPI.Authentication;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FCG.WebAPI.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IConfiguration _configuration;
        public LoginController(IMediator mediator, IConfiguration configuration) 
        {
            _mediator = mediator;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUsuarioRequest loginUsuarioRequest)
        {
            try
            {
                var usuario = await _mediator.Send(loginUsuarioRequest);
                AuthenticationToken authenticationToken = new AuthenticationToken(_configuration);
                var token = authenticationToken.GenerateToken(usuario);
                return Ok(new { token });
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
