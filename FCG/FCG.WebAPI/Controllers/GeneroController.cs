using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// Dependências
using FCG.Application.UseCases.Feature.Jogo.Commands.AddGenero;
using FCG.Application.UseCases.Feature.Jogo.Commands.DeleteGenero;
using FCG.Application.UseCases.Feature.Jogo.Commands.EditGenero;
using FCG.Application.UseCases.Feature.Jogo.Queries.GetAllGenero;
using FCG.Application.UseCases.Feature.Jogo.Queries.GetGenero;

namespace FCG.WebAPI.Controllers
{
    /// <summary>
    /// Gênero
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "ADMINISTRADOR")]
    public class GeneroController : ControllerBase
    {
        private readonly IMediator _mediator;

        public GeneroController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Incluir")]
        public async Task<IActionResult> IncluirGenero(AddGeneroCommand addGeneroCommand)
        {
            var genero = await _mediator.Send(addGeneroCommand);

            return CreatedAtAction("IncluirGenero", genero);
        }

        [HttpPut("Alterar")]
        public async Task<IActionResult> AlterarGenero([FromBody] EditGeneroCommand editGeneroCommand)
        {
            var genero = await _mediator.Send(editGeneroCommand);

            return CreatedAtAction("AlterarGenero", genero);
        }

        [HttpDelete("Deletar{id}")]
        public async Task<IActionResult> DeletarGenero(int id)
        {
            var isDeleted = await _mediator.Send(new DeleteGeneroCommand { Id = id });
            if (isDeleted)
            {
                return Ok("Gênero foi deletado com sucesso");
            }

            return NotFound();
        }

        [HttpGet("Obter{id}")]
        public async Task<IActionResult> ObterGenero(int id)
        {
            var genero = await _mediator.Send(new GetGeneroQuery { Id = id });

            return CreatedAtAction("ObterGenero", genero);
        }

        /// <summary>
        /// Obter todos os gêneros
        /// </summary>
        /// <returns></returns>
        [HttpGet("ObterTodos")]
        public async Task<IActionResult> ObterTodosGeneros()
        {
            var genero = await _mediator.Send(new GetAllGeneroQuery());

            return Ok(genero);
        }
    }
}
