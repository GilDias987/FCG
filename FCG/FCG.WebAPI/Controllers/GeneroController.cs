// Dependências

using FCG.Application.UseCases.Feature.Jogo.Commands.AddGenero;
using FCG.Application.UseCases.Feature.Jogo.Commands.DeleteGenero;
using FCG.Application.UseCases.Feature.Jogo.Commands.EditJGenero;
using FCG.Application.UseCases.Feature.Jogo.Queries.GetGenero;
using FCG.Domain.Entities;
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
    //[Authorize(Policy = "ADMINISTRADOR")]
    public class GeneroController : ControllerBase
    {
        private readonly IMediator _mediator;

        public GeneroController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> IncluirGenero(AddGeneroCommand addGeneroCommand)
        {
            var genero = await _mediator.Send(addGeneroCommand);

            return CreatedAtAction("IncluirGenero", genero);
        }

        [HttpPut()]
        public async Task<IActionResult> AlterarGenero([FromBody] EditGeneroCommand editGeneroCommand)
        {
            var genero = await _mediator.Send(editGeneroCommand);

            return CreatedAtAction("AlterarGenero", genero);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarGenero(int id)
        {
            var isDeleted = await _mediator.Send(new DeleteGeneroCommand { Id = id });
            if (isDeleted)
            {
                return Ok("Gênero foi deletado com sucesso");
            }

            return NotFound();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterGenero(int id)
        {
            var genero = await _mediator.Send(new GetGeneroQuery { Id = id });

            return CreatedAtAction("ObterGenero", genero);
        }
    }
}
