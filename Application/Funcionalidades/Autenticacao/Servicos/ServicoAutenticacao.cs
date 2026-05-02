using Application.Funcionalidades.Autenticacao.Dtos;
using Application.Funcionalidades.Autenticacao.Contratos.CasosDeUso;

namespace Application.Funcionalidades.Autenticacao.Servicos
{
    public class ServicoAutenticacao : IServicoAutenticacao
    {
        private readonly ILoginCasoDeUso _loginUseCase;

        public ServicoAutenticacao(
            ILoginCasoDeUso loginUseCase)
        {
            _loginUseCase = loginUseCase;
        }

        public Task<AutenticacaoResposta> Login(AutenticacaoRequisicao request) 
            =>  _loginUseCase.Executar(request);

    }
}

