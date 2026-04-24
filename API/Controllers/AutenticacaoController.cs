using Application.Dtos.Autenticacaos;
using Application.Services.ServAutenticacaos;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutenticacaoController : ControllerBase
    {
        private readonly IServAutenticacao _aplic;

        public AutenticacaoController(
            IServAutenticacao aplic)
        {
            _aplic = aplic;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(RequestAutenticacaoRequest request)
        {
            var token = await _aplic.Login(request);

            return Ok(new { token });
        }
    }
}
