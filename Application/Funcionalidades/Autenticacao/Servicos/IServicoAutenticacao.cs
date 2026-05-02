using Application.Funcionalidades.Autenticacao.Dtos;

namespace Application.Funcionalidades.Autenticacao.Servicos
{
    public interface IServicoAutenticacao
    {
        public Task<AutenticacaoResposta> Login(AutenticacaoRequisicao request);
    }
}

