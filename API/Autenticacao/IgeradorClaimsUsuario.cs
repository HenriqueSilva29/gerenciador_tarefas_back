using Application.Funcionalidades.Autenticacao.Dtos;
using System.Security.Claims;

namespace API.Autenticacao
{
    public interface IGeradorClaimsUsuario
    {
        public ClaimsPrincipal Gerar(AutenticacaoResposta usuario);
    }
}
