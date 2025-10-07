using FCG.ApplicationCore.Dto.Autenticacao.GrupoUsuario;
using FCG.ApplicationCore.Interface.Service;
using FCG.ApplicationCore.Service;
using FCG.Domain.Entity;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace FCG.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GrupoUsuarioController : ControllerBase
    {
        private readonly IGrupoUsuarioService _grupoUsuarioService;
        public GrupoUsuarioController(IGrupoUsuarioService grupoUsuarioService)
        {
            _grupoUsuarioService = grupoUsuarioService;
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarGrupoUsuario([FromBody] AddGrupoUsuarioDto GrupoUsuario)
        {
            try
            {
                await _grupoUsuarioService.CadastrarAsync(GrupoUsuario);
                return Ok("Grupo de Usuario cadastrado com sucesso");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpPut("{id}")]
        public async Task<ActionResult> EditarGrupoUsuario(int id, [FromBody] AddGrupoUsuarioDto GrupoUsuario)
        {
            try
            {
                await _grupoUsuarioService.EditarAsync(id, GrupoUsuario);
                return Ok("Grupo de Usuario editado com sucesso");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletarGrupoUsuario(int id)
        {
            try
            {
                await _grupoUsuarioService.ExcluirAsync(id);
                return Ok("Grupo de Usuario excluído com sucesso");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
