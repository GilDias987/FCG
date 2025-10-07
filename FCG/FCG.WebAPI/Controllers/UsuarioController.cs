using FCG.ApplicationCore.Dto.Autenticacao.GrupoUsuario;
using FCG.ApplicationCore.Dto.Autenticacao.Usuario;
using FCG.ApplicationCore.Interface.Service;
using Microsoft.AspNetCore.Mvc;

namespace FCG.WebAPI.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : Controller
    {
        private readonly IUsuarioService _usuarioService;
        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarUsuario([FromBody] AddUsuarioDto addUsuarioDto)
        {
            try
            {
                await _usuarioService.CadastrarAsync(addUsuarioDto);
                return Ok("Grupo de Usuario cadastrado com sucesso");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
