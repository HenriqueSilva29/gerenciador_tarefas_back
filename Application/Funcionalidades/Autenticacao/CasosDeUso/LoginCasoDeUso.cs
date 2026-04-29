using Application.Funcionalidades.Autenticacao.Dtos;
using Application.Funcionalidades.Autenticacao.Contratos.CasosDeUso;
using Domain.Enumeradores;
using Domain.Excecoes;
using Microsoft.AspNetCore.Http;
using Repository.Repositorios.Usuarios;

namespace Application.Funcionalidades.Autenticacao.CasosDeUso
{
    public class LoginCasoDeUso : ILoginCasoDeUso
    {
        private readonly IRepUsuario _rep;
        private readonly IGerarTokenCasoDeUso _gerarToken;
        private readonly IVerificarSenhaCasoDeUso _verificarSenha;

        public LoginCasoDeUso(
            IRepUsuario rep,
            IGerarTokenCasoDeUso gerarToken,
            IVerificarSenhaCasoDeUso verificarSenha)
        {
            _rep = rep;
            _gerarToken = gerarToken;
            _verificarSenha =verificarSenha;
        }

        public async Task<string> Executar(AutenticacaoRequisicao request)
        {
            var usuario = await _rep.ObterUsuarioPorEmail(request.Email);

            if (usuario == null)
                throw new ExcecaoAplicacao(
                    EnumCodigosDeExcecao.CredenciaisInvalidas,
                    "Email ou senha invalidos",
                    StatusCodes.Status401Unauthorized);

            var senhaValida = _verificarSenha.Executar(usuario, request.Senha);

            if (!senhaValida)
                throw new ExcecaoAplicacao(
                    EnumCodigosDeExcecao.CredenciaisInvalidas,
                    "Email ou senha invalidos",
                    StatusCodes.Status401Unauthorized);

            var token = _gerarToken.Executar(usuario);

            return token;
        }
    }
}



