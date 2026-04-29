using Domain.Entidades;

namespace Application.Funcionalidades.ParamGerais.Contratos.CasosDeUso
{
    public interface IListarParamGeralCasoDeUso
    {
        Task<ParamGeral> ExecutarAsync();
    }
}


