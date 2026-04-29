using Domain.Comum.ObjetosDeValor;

namespace Application.Funcionalidades.Lembretes.Contratos.CasosDeUso
{
    public interface IAgendarLembreteCasoDeUso
    {
        Task ExecuteAsync(int id, UtcDateTime dataDisparo); 
    }
}


