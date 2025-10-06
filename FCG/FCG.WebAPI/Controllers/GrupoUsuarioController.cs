using FCG.ApplicationCore.Repository;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace FCG.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GrupoUsuarioController : ControllerBase
    {
        private readonly IGrupoUsuarioRepository _grupoUsuarioRepository;

        public GrupoUsuarioController(IGrupoUsuarioRepository grupoUsuarioRepository)
        {
            _grupoUsuarioRepository = grupoUsuarioRepository;
        }

        [HttpPost]
        public IActionResult Criar()
        {
            try
            {
                return Ok("Deu certo");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
