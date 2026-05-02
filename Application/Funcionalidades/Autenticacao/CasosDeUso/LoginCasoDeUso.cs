using Application.Funcionalidades.Autenticacao.Contratos.CasosDeUso;
using Application.Funcionalidades.Autenticacao.Dtos;
using Domain.Enumeradores;
using Domain.Excecoes;
using Microsoft.AspNetCore.Http;
using Repository.Repositorios.Usuarios;

namespace Application.Funcionalidades.Autenticacao.CasosDeUso
{
    public class LoginCasoDeUso : ILoginCasoDeUso
    {
        private readonly IRepUsuario _rep;
        private readonly IVerificarSenhaCasoDeUso _verificarSenha;

        public LoginCasoDeUso(
            IRepUsuario rep,
            IVerificarSenhaCasoDeUso verificarSenha)
        {
            _rep = rep;
            _verificarSenha = verificarSenha;
        }

        public async Task<AutenticacaoResposta> Executar(AutenticacaoRequisicao request)
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

            return new AutenticacaoResposta
            {
                Id = usuario.Id,
                Email = usuario.Email,
                Role = usuario.Role,
                Nome = usuario.Nome
            };
        }
    }
}
