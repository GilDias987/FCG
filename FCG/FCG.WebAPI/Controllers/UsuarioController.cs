using FCG.ApplicationCore.Dto.Autenticacao.Usuario;
using FCG.ApplicationCore.Feature.Usuario.Query.GetUsuario;
using FCG.ApplicationCore.Feature.Usuario.Query.LoginUsuario;
using FCG.ApplicationCore.Interface.Service;
using FCG.ApplicationCore.Service;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FCG.WebAPI.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Policy = "ADMINISTRADOR")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        private readonly IMediator _mediator;
        public UsuarioController(IUsuarioService usuarioService, IMediator mediator)
        {
            _usuarioService = usuarioService;
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarUsuario([FromBody] AddUsuarioDto addUsuarioDto)
        {
            try
            {
                await _usuarioService.CadastrarAsync(addUsuarioDto);
                return Ok("Usuario cadastrado com sucesso");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterUsuario(int id)
        {
            try
            {
                var usuario = await _mediator.Send(new GetUsuarioRequest { Id = id });
                return Ok(usuario);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
