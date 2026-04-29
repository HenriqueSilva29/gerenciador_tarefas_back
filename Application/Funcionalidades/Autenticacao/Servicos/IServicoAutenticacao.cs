using Application.Funcionalidades.Autenticacao.Dtos;

namespace Application.Funcionalidades.Autenticacao.Servicos
{
    public interface IServicoAutenticacao
    {
        public Task<string> Login(AutenticacaoRequisicao request);
    }
}

