using MediatR;
using Microsoft.AspNetCore.Mvc;

// Depenências
using FCG.WebAPI.Authentication;
using FCG.Application.UseCases.Feature.Usuario.Queries.LoginUsuario;

namespace FCG.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
