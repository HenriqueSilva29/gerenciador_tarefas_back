using Application.Dtos.Autenticacao;
using Application.Interfaces.UseCases.Autenticacaos;
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

        public async Task<string> Executar(RequestAutenticacaoDto request)
        {
            var usuario = await _rep.ObterUsuarioPorEmail(request.Email);

            if (usuario == null)
                throw new ApplicationException("Usuário ou senha inválidos");

            var senhaValida = _verificarSenha.Executar(usuario, request.Senha);

            if (!senhaValida)
                throw new ApplicationException("Usuário ou senha inválidos");

            var token = _gerarToken.Executar(usuario);

            return token;
        }
    }
}
