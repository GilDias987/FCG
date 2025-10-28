using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// Dependências
using FCG.Application.UseCases.Feature.Jogo.Commands.AddJogo;
using FCG.Application.UseCases.Feature.Jogo.Commands.DeleteJogo;
using FCG.Application.UseCases.Feature.Jogo.Commands.EditJogo;
using FCG.Application.UseCases.Feature.Jogo.Queries.GetJogo;
using FCG.Application.UseCases.Feature.Jogo.Queries.GetAllJogo;

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

        /// <summary>
        /// Incluir
        /// </summary>
        /// <param name="addJogoCommand"></param>
        /// <returns></returns>
        [HttpPost("Incluir")]
        public async Task<IActionResult> IncluirJogo(AddJogoCommand addJogoCommand)
        {
            var jogo = await _mediator.Send(addJogoCommand);

            return CreatedAtAction("IncluirJogo", jogo);
        }

        /// <summary>
        /// Alterar
        /// </summary>
        /// <param name="editJogoCommand"></param>
        /// <returns></returns>
        [HttpPut("Alterar")]
        public async Task<IActionResult> AlterarJogo([FromBody] EditJogoCommand editJogoCommand)
        {
            var jogo = await _mediator.Send(editJogoCommand);

            return CreatedAtAction("AlterarJogo", jogo);
        }

        /// <summary>
        /// Deletar
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Obter
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Obter{id}")]
        public async Task<IActionResult> ObterJogo(int id)
        {
            var jogo = await _mediator.Send(new GetJogoQuery { Id = id });

            return CreatedAtAction("ObterJogo", jogo);
        }

        /// <summary>
        /// Obter todos os jogos
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
