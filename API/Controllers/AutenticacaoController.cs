using API.Autenticacao;
using Application.Funcionalidades.Autenticacao.Dtos;
using Application.Funcionalidades.Autenticacao.Servicos;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutenticacaoController : ControllerBase
    {
        private readonly IServicoAutenticacao _aplic;
        private readonly IGeradorClaimsUsuario _geradorClaimsUsuario;

        public AutenticacaoController(IServicoAutenticacao aplic, IGeradorClaimsUsuario geradorClaimsUsuario)
        {
            _aplic = aplic;
            _geradorClaimsUsuario= geradorClaimsUsuario;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(AutenticacaoRequisicao request)
        {
            var usuario = await _aplic.Login(request);

            var claimsPrincipal = _geradorClaimsUsuario.Gerar(usuario);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                claimsPrincipal,
                new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddHours(1)
                });

            return Ok(usuario);
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Ok();
        }
    }
}
