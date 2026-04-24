using Application.Dtos.Autenticacaos;
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

        public Task<string> Login(RequestAutenticacaoRequest request) 
            =>  _loginUseCase.Executar(request);

    }
}
