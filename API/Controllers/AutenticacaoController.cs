using Application.Funcionalidades.Autenticacao.Dtos;
using Application.Funcionalidades.Autenticacao.Servicos;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutenticacaoController : ControllerBase
    {
        private readonly IServicoAutenticacao _aplic;

        public AutenticacaoController(
            IServicoAutenticacao aplic)
        {
            _aplic = aplic;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(AutenticacaoRequisicao request)
        {
            var token = await _aplic.Login(request);

            return Ok(new { token });
        }
    }
}


