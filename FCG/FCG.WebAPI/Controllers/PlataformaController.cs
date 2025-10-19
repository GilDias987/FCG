using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// Dependências
using FCG.ApplicationCore.Feature.Jogo.Commands.AddPlataforma;
using FCG.ApplicationCore.Feature.Jogo.Commands.DeletePlataforma;
using FCG.ApplicationCore.Feature.Jogo.Commands.EditPlataforma;
using FCG.ApplicationCore.Feature.Jogo.Queries.GetPlataforma;

namespace FCG.WebAPI.Controllers
{
    /// <summary>
    /// Gênero
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Policy = "ADMINISTRADOR")]
    public class PlataformaController : BaseController
    {
        private readonly IMediator _mediator;

        public PlataformaController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> IncluirPlataforma(AddPlataformaCommand addPlataformaCommand)
        {
            var plataforma = await _mediator.Send(addPlataformaCommand);

            return CreatedAtAction("IncluirPlataforma", plataforma);
        }

        [HttpPut()]
        public async Task<IActionResult> AlterarPlataforma([FromBody] EditPlataformaCommand editPlataformaCommand)
        {
            var plataforma = await _mediator.Send(editPlataformaCommand);

            return CreatedAtAction("AlterarPlataforma", plataforma);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarPlataforma(int id)
        {
            var isDeleted = await _mediator.Send(new DeletePlataformaCommand { Id = id });
            if (isDeleted)
            {
                return Ok("Plataforma foi deletado com sucesso");
            }

            return NotFound();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterPlataforma(int id)
        {
            var plataforma = await _mediator.Send(new GetPlataformaQuery { Id = id });

            return CreatedAtAction("ObterPlataforma", plataforma);
        }
    }
}
