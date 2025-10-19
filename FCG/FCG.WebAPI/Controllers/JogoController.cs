// Dependências
using FCG.ApplicationCore.Feature.Jogo.Commands.AddJogo;
using FCG.ApplicationCore.Feature.Jogo.Commands.DeleteJogo;
using FCG.ApplicationCore.Feature.Jogo.Commands.EditJogo;
using FCG.ApplicationCore.Feature.Jogo.Queries.GetJogo;
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
    //[Authorize(Policy = "ADMINISTRADOR")]
    public class JogoController : BaseController
    {
        private readonly IMediator _mediator;

        public JogoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> IncluirJogo(AddJogoCommand addJogoCommand)
        {
            var jogo = await _mediator.Send(addJogoCommand);

            return CreatedAtAction("IncluirJogo", jogo);
        }

        [HttpPut()]
        public async Task<IActionResult> AlterarJogo([FromBody] EditJogoCommand editJogoCommand)
        {
            var jogo = await _mediator.Send(editJogoCommand);

            return CreatedAtAction("AlterarJogo", jogo);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarJogo(int id)
        {
            var isDeleted = await _mediator.Send(new DeleteJogoCommand { Id = id });
            if (isDeleted)
            {
                return Ok("Jogo foi deletado com sucesso");
            }

            return NotFound();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterJogo(int id)
        {
            var jogo = await _mediator.Send(new GetJogoQuery { Id = id });

            return CreatedAtAction("ObterJogo", jogo);
        }
    }
}
