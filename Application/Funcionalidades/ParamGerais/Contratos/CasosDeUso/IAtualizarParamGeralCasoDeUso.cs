using Application.Funcionalidades.ParamGerais.Dtos;

namespace Application.Funcionalidades.ParamGerais.Contratos.CasosDeUso
{
    public interface IAtualizarParamGeralCasoDeUso
    {
        Task ExecuteAsync(AtualizarParamGeralRequisicao dto);
    }
}

