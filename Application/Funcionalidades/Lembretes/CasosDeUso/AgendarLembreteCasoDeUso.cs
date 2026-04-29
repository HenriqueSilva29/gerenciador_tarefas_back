using Application.Funcionalidades.Lembretes.Contratos.CasosDeUso;
using Domain.Comum.ObjetosDeValor;
using Hangfire;

namespace Application.Funcionalidades.Lembretes.CasosDeUso
{
    public class AgendarLembreteCasoDeUso : IAgendarLembreteCasoDeUso
    {
        private readonly IDispararLembreteCasoDeUso _dispararLembreteUseCase;
        public AgendarLembreteCasoDeUso(IDispararLembreteCasoDeUso dispararLembreteUseCase)
        {
            _dispararLembreteUseCase = dispararLembreteUseCase;
        }

        public async Task ExecuteAsync(int id, UtcDateTime dataDisparo)
        {
            BackgroundJob.Schedule(
            () => _dispararLembreteUseCase.ExecuteAsync(id),
            dataDisparo
            );
        }
    }
}


