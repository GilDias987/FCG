using Microsoft.AspNetCore.Mvc;

// Dependências
using FCG.Helper;

namespace FCG.WebAPI.Controllers
{
    public class BaseController : ControllerBase
    {
        public IActionResult ReturnFormattedResponse<T>(ServiceResponse<T> response)
        {
            if (response.Success)
            {
                return Ok(response.Data);
            }
            return StatusCode(response.StatusCode, response.Errors);
        }
    }
}
