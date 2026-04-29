using Application.Funcionalidades.Lembretes.Contratos.CasosDeUso;
using Domain.Comum.ObjetosDeValor;

namespace Infra.BackgroundJobs.Hangfire.Jobs.Lembretes
{
    public class AgendarLembreteJobScheduler : IAgendadorJobLembrete
    {
        private readonly IAgendarLembreteCasoDeUso _agendarLembreteUseCase;

        public AgendarLembreteJobScheduler
        (
            IAgendarLembreteCasoDeUso agendarLembreteUseCase
        )
        {
            _agendarLembreteUseCase = agendarLembreteUseCase;
        }
        public async Task ExecuteAsync(int id, UtcDateTime dataDisparo)
        {
            await _agendarLembreteUseCase.ExecuteAsync(id, dataDisparo);
        }
    }
}

