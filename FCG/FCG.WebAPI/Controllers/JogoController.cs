// Dependências
using FCG.Application.UseCases.Feature.Jogo.Commands.AddJogo;
using FCG.Application.UseCases.Feature.Jogo.Commands.DeleteJogo;
using FCG.Application.UseCases.Feature.Jogo.Commands.EditJogo;
using FCG.Application.UseCases.Feature.Jogo.Queries.GetJogo;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FCG.WebAPI.Controllers
{
    //https://developer.mozilla.org/en-US/docs/Web/HTTP/Reference/Status/400

    /// <summary>
    /// Usuário
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class JogoController : ControllerBase
    {
        private readonly IMediator _mediator;

        public JogoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Incluir")]
        public async Task<IActionResult> IncluirJogo(AddJogoCommand addJogoCommand)
        {
            var jogo = await _mediator.Send(addJogoCommand);

            return CreatedAtAction("IncluirJogo", jogo);
        }

        [HttpPut("Alterar")]
        public async Task<IActionResult> AlterarJogo([FromBody] EditJogoCommand editJogoCommand)
        {
            var jogo = await _mediator.Send(editJogoCommand);

            return CreatedAtAction("AlterarJogo", jogo);
        }

        [HttpDelete("Deletar{id}")]
        public async Task<IActionResult> DeletarJogo(int id)
        {
            var isDeleted = await _mediator.Send(new DeleteJogoCommand { Id = id });
            if (isDeleted)
            {
                return Ok("Jogo foi deletado com sucesso");
            }

            return NotFound();
        }

        [HttpGet("Obter{id}")]
        public async Task<IActionResult> ObterJogo(int id)
        {
            var jogo = await _mediator.Send(new GetJogoQuery { Id = id });

            return CreatedAtAction("ObterJogo", jogo);
        }

        /// <summary>
        /// Obter todos jogos
        /// </summary>
        /// <returns></returns>
        [HttpGet("ObterTodos")]
        public async Task<IActionResult> ObterTodosJogos()
        {
            var jogo = await _mediator.Send(new GetAllJogoQuery());

            return Ok(jogo);
        }
    }
}
