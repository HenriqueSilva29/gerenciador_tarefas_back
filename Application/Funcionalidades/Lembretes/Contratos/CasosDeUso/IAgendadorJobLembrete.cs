using Domain.Comum.ObjetosDeValor;

namespace Application.Funcionalidades.Lembretes.Contratos.CasosDeUso
{
    public interface IAgendadorJobLembrete
    {
        Task ExecuteAsync(int id, UtcDateTime dataDisparo);
    }
}


