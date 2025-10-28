using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// Dependências
using FCG.Application.UseCases.Feature.Usuario.Commands.AddUsuario;
using FCG.Application.UseCases.Feature.Usuario.Commands.DeleteUsuario;
using FCG.Application.UseCases.Feature.Usuario.Commands.EditUsuario;
using FCG.Application.UseCases.Feature.Usuario.Queries.GetAllUsuario;
using FCG.Application.UseCases.Feature.Usuario.Queries.GetUsuario;

namespace FCG.WebAPI.Controllers
{
    /// <summary>
    /// Usuário
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Policy = "ADMINISTRADOR")]
    public class UsuarioController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsuarioController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Incluir Usuário
        /// </summary>
        /// <param name="addUsuarioCommand"></param>
        /// <returns></returns>
        [HttpPost("Incluir")]
        public async Task<IActionResult> IncluirUsuario(AddUsuarioCommand addUsuarioCommand)
        {
            var usuario = await _mediator.Send(addUsuarioCommand);

            return Created($"/api/usuario/{usuario.Id}", usuario);
        }

        /// <summary>
        /// Alterar Usuário
        /// </summary>
        /// <param name="editUsuarioCommand"></param>
        /// <returns></returns>
        [HttpPut("Alterar")]
        public async Task<IActionResult> AlterarUsuario([FromBody] EditUsuarioCommand editUsuarioCommand)
        {
            var usuario = await _mediator.Send(editUsuarioCommand);

            return Ok(usuario);
        }

        /// <summary>
        /// Deletar Usuário
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("Deletar{id}")]
        public async Task<IActionResult> DeletarUsuario(int id)
        {
            var isDeleted = await _mediator.Send(new DeleteUsuarioCommand { Id = id });
            if (isDeleted)
            {
                return Ok("Usuário deletado com sucesso");
            }

            return NotFound();
        }

        /// <summary>
        /// Obter Usuário
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Obter{id}")]
        public async Task<IActionResult> ObterUsuario(int id)
        {
            var usuario = await _mediator.Send(new GetUsuarioQuery { Id = id });

            return Ok(usuario);
        }

        /// <summary>
        /// Obter todos os Usuários
        /// </summary>
        /// <returns></returns>
        [HttpGet("ObterTodos")]
        public async Task<IActionResult> ObterTodosUsuarios()
        {
            var usuario = await _mediator.Send(new GetAllUsuarioQuery());

            return Ok(usuario);
        }
    }
}
