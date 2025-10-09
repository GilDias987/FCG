using FCG.ApplicationCore.Feature.Usuario.Command.AddGrupoUsuario;
using FCG.ApplicationCore.Feature.Usuario.Command.DeleteGrupoUsuario;
using FCG.ApplicationCore.Feature.Usuario.Command.EditGrupoUsuario;
using FCG.ApplicationCore.Feature.Usuario.Query.GetGrupoUsuario;
using FCG.ApplicationCore.Feature.Usuario.Query.LIstGrupoUsuario;
using FCG.ApplicationCore.Interface.Service;
using FCG.ApplicationCore.Service;
using FCG.Domain.Entity;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace FCG.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class GrupoUsuarioController : ControllerBase
    {
        private readonly IMediator _mediator;
        public GrupoUsuarioController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarGrupoUsuario([FromBody] AddGrupoUsuarioCommand addGrupoUsuarioCommand)
        {
            try
            {
                var grupoUsuarioId = await _mediator.Send(addGrupoUsuarioCommand);
                var grupo = await ObterGrupoUsuario(grupoUsuarioId);
                return grupo;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPut()]
        public async Task<IActionResult> EditarGrupoUsuario([FromBody] EditGrupoUsuarioCommand editGrupoUsuarioCommand)
        {
            try
            {
                var grupoUsuarioId = await _mediator.Send(editGrupoUsuarioCommand);
                var grupo = await ObterGrupoUsuario(grupoUsuarioId);
                return grupo;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletarGrupoUsuario(int id)
        {
            try
            {
                await _mediator.Send(new DeleteGrupoUsuarioCommand { Id = id });
                return Ok("Grupo de Usuario deletado com sucesso");
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterGrupoUsuario(int id)
        {
            try
            {
                var grupoUsuario = await _mediator.Send(new GetGrupoUsuarioRequest { Id = id});
                return Ok(grupoUsuario);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("ListarGruposUsuario")]
        public async Task<IActionResult> ListarGrupoUsuario()
        {
            try
            {
                var lstGrupoUsuario = await _mediator.Send(new ListGrupoUsuarioRequest());
                return Ok(lstGrupoUsuario);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
