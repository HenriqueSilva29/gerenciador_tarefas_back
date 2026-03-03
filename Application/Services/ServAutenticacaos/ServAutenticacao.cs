using Application.Dtos.Autenticacao;
using Application.Interfaces.UseCases.Autenticacaos;

namespace Application.Services.ServAutenticacaos
{
    public class ServAutenticacao : IServAutenticacao
    {
        private readonly ILoginUseCase _loginUseCase;

        public ServAutenticacao(
            ILoginUseCase loginUseCase)
        {
            _loginUseCase = loginUseCase;
        }

        public async Task<string> Login(RequestAutenticacaoDto request) 
            => await _loginUseCase.Executar(request);

    }
}
