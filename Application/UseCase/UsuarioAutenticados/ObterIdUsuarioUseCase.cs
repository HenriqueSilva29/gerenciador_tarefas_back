using Application.Interfaces.Context;
using Application.Interfaces.UseCases.UsuarioAutenticados;
using Domain.Enums;
using Domain.Exceptions;
using Microsoft.AspNetCore.Http;

namespace Application.UseCase.UsuarioAutenticados
{
    public class ObterIdUsuarioUseCase : IObterIdUsuarioUseCase
    {
        private readonly IUsuarioContexto _usuarioContexto;
        public ObterIdUsuarioUseCase(IUsuarioContexto usuarioContexto)
        {

            _usuarioContexto = usuarioContexto;

        }
        public int Execute()
        {
            if (_usuarioContexto.IdUsuario.HasValue)
                return _usuarioContexto.IdUsuario.Value;

            throw new ExceptionApplication(
                EnumCodigosDeExcecao.CredenciaisInvalidas,
                "Usuario autenticado nao identificado.",
                StatusCodes.Status401Unauthorized);
        }
    }
}
