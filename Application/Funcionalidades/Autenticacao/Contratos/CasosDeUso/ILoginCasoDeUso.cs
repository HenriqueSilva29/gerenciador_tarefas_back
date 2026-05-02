using Application.Funcionalidades.Autenticacao.Dtos;

namespace Application.Funcionalidades.Autenticacao.Contratos.CasosDeUso
{
    public interface ILoginCasoDeUso
    {
        public Task<AutenticacaoResposta> Executar(AutenticacaoRequisicao request);
    }
}

