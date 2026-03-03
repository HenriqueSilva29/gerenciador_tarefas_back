using Application.Dtos.Autenticacao;
using Application.Interfaces.UseCases.Autenticacaos;
using Application.Services.ServAutenticacaos;
using Microsoft.AspNetCore.Mvc;
using Repository.Repositorys.UsuarioRep;

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
        public async Task<IActionResult> Login(RequestAutenticacaoDto request)
        {
            var token = _aplic.Login(request);

            return Ok(new { token });
        }
    }
}
