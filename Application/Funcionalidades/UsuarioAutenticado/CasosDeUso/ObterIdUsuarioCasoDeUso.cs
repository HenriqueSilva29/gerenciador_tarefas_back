using Application.Interfaces.Context;
using Application.Funcionalidades.UsuarioAutenticado.Contratos.CasosDeUso;
using Domain.Enumeradores;
using Domain.Excecoes;
using Microsoft.AspNetCore.Http;

namespace Application.Funcionalidades.UsuarioAutenticado.CasosDeUso
{
    public class ObterIdUsuarioCasoDeUso : IObterIdUsuarioCasoDeUso
    {
        private readonly IUsuarioContexto _usuarioContexto;
        public ObterIdUsuarioCasoDeUso(IUsuarioContexto usuarioContexto)
        {

            _usuarioContexto = usuarioContexto;

        }
        public int Execute()
        {
            if (_usuarioContexto.IdUsuario.HasValue)
                return _usuarioContexto.IdUsuario.Value;

            throw new ExcecaoAplicacao(
                EnumCodigosDeExcecao.UsuarioNaoAutenticado,
                "Usuario autenticado nao identificado.",
                StatusCodes.Status401Unauthorized);
        }
    }
}


