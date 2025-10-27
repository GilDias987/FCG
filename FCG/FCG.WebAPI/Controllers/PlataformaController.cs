using FCG.Application.UseCases.Feature.Jogo.Commands.AddPlataforma;
using FCG.Application.UseCases.Feature.Jogo.Commands.DeletePlataforma;
using FCG.Application.UseCases.Feature.Jogo.Commands.EditPlataforma;
using FCG.Application.UseCases.Feature.Jogo.Queries.GetPlataforma;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FCG.WebAPI.Controllers
{
    /// <summary>
    /// Gênero
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "ADMINISTRADOR")]
    public class PlataformaController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PlataformaController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Incluir")]
        public async Task<IActionResult> IncluirPlataforma(AddPlataformaCommand addPlataformaCommand)
        {
            var plataforma = await _mediator.Send(addPlataformaCommand);

            return CreatedAtAction("IncluirPlataforma", plataforma);
        }

        [HttpPut("Alterar")]
        public async Task<IActionResult> AlterarPlataforma([FromBody] EditPlataformaCommand editPlataformaCommand)
        {
            var plataforma = await _mediator.Send(editPlataformaCommand);

            return CreatedAtAction("AlterarPlataforma", plataforma);
        }

        [HttpDelete("Deletar{id}")]
        public async Task<IActionResult> DeletarPlataforma(int id)
        {
            var isDeleted = await _mediator.Send(new DeletePlataformaCommand { Id = id });
            if (isDeleted)
            {
                return Ok("Plataforma foi deletado com sucesso");
            }

            return NotFound();
        }

        [HttpGet("Obter{id}")]
        public async Task<IActionResult> ObterPlataforma(int id)
        {
            var plataforma = await _mediator.Send(new GetPlataformaQuery { Id = id });

            return CreatedAtAction("ObterPlataforma", plataforma);
        }

        /// <summary>
        /// Obter todas plataformas
        /// </summary>
        /// <returns></returns>
        [HttpGet("ObterTodos")]
        public async Task<IActionResult> ObterTodosPlataformas()
        {
            var genero = await _mediator.Send(new GetAllPlataformaQuery());

            return Ok(genero);
        }
    }
}
