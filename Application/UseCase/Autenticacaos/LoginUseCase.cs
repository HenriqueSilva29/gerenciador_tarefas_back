using Application.Dtos.Autenticacaos;
using Application.Interfaces.UseCases.Autenticacaos;
using Domain.Enums;
using Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Repository.Repositorys.UsuarioRep;

namespace Application.UseCase.Autenticacaos
{
    public class LoginUseCase : ILoginUseCase
    {
        private readonly IRepUsuario _rep;
        private readonly IGerarTokenUseCase _gerarToken;
        private readonly IVerificarSenhaUseCase _verificarSenha;

        public LoginUseCase(
            IRepUsuario rep,
            IGerarTokenUseCase gerarToken,
            IVerificarSenhaUseCase verificarSenha)
        {
            _rep = rep;
            _gerarToken = gerarToken;
            _verificarSenha =verificarSenha;
        }

        public async Task<string> Executar(RequestAutenticacaoRequest request)
        {
            var usuario = await _rep.ObterUsuarioPorEmail(request.Email);

            if (usuario == null)
                throw new ExceptionApplication(EnumCodigosDeExcecao.CredenciaisInvalidas, "Email ou senha inválidos", StatusCodes.Status409Conflict);

            var senhaValida = _verificarSenha.Executar(usuario, request.Senha);

            if (!senhaValida)
                throw new ExceptionApplication(EnumCodigosDeExcecao.CredenciaisInvalidas, "Email ou senha inválidos", StatusCodes.Status409Conflict);

            var token = _gerarToken.Executar(usuario);

            return token;
        }
    }
}
