// Dependências
using FCG.Application.UseCases.Feature.Usuario.Commands.AddGrupoUsuario;
using FCG.Application.UseCases.Feature.Usuario.Commands.DeleteGrupoUsuario;
using FCG.Application.UseCases.Feature.Usuario.Commands.EditGrupoUsuario;
using FCG.Application.UseCases.Feature.Usuario.Commands.EditUsuario;
using FCG.Application.UseCases.Feature.Usuario.Queries.GetGrupoUsuario;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FCG.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Policy = "ADMINISTRADOR")]
    public class GrupoUsuarioController : ControllerBase
    {
        private readonly IMediator _mediator;
        public GrupoUsuarioController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Incluir
        /// </summary>
        /// <param name="addGrupoUsuarioCommand"></param>
        /// <returns></returns>
        [HttpPost("Incluir")]
        public async Task<IActionResult> IncluirGrupoUsuario([FromBody] AddGrupoUsuarioCommand addGrupoUsuarioCommand)
        {
            var grupoUsuario = await _mediator.Send(addGrupoUsuarioCommand);
            return CreatedAtAction("IncluirGrupoUsuario", grupoUsuario);
        }

        /// <summary>
        /// Altearar
        /// </summary>
        /// <param name="editGrupoUsuarioCommand"></param>
        /// <returns></returns>
        [HttpPut("Alterar")]
        public async Task<IActionResult> AlterarGrupoUsuario([FromBody] EditGrupoUsuarioCommand editGrupoUsuarioCommand)
        {
            var grupoUsuario = await _mediator.Send(editGrupoUsuarioCommand);
            return Ok(grupoUsuario);
        }

        /// <summary>
        /// Deletar
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("Deletar{id}")]
        public async Task<ActionResult> DeletarGrupoUsuario(int id)
        {
            var isDeleted = await _mediator.Send(new DeleteGrupoUsuarioCommand { Id = id });
           
            if (isDeleted)
            {
                return Ok("Grupo de Usuario foi deletado com sucesso.");
            }

            return NotFound();
        }

        /// <summary>
        /// Obter
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Obter{id}")]
        public async Task<IActionResult> ObterGrupoUsuario(int id)
        {
            var grupoUsuario = await _mediator.Send(new GetGrupoUsuarioQuery { Id = id });

            return CreatedAtAction("ObterGrupoUsuario", grupoUsuario);
        }

        /// <summary>
        /// Obter todos grupos de usuários
        /// </summary>
        /// <returns></returns>
        [HttpGet("ObterTodos")]
        public async Task<IActionResult> ObterTodosGrupoUsuario()
        {
            var grupoUsuario = await _mediator.Send(new GetAllGrupoUsuarioQuery());

            return Ok(grupoUsuario);
        }
    }
}
