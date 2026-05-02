using Application.Funcionalidades.Autenticacao.Dtos;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace API.Autenticacao
{
    public class GeradorClaimsUsuario : IGeradorClaimsUsuario
    {
        public ClaimsPrincipal Gerar(AutenticacaoResposta usuario)
        {
            var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
            new(ClaimTypes.Email, usuario.Email),
            new(ClaimTypes.Role, usuario.Role)
        };

            if (!string.IsNullOrWhiteSpace(usuario.Nome))
                claims.Add(new Claim(ClaimTypes.Name, usuario.Nome));

            var identity = new ClaimsIdentity(
                claims,
                CookieAuthenticationDefaults.AuthenticationScheme);

            return new ClaimsPrincipal(identity);
        }
    }
}
