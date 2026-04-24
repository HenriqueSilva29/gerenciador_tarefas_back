using Application.Interfaces.UseCases.Lembretes;
using Domain.Common.ValueObjects;

namespace Infra.BackgroundJobs.Hangfire.Jobs.Lembretes
{
    public class AgendarLembreteJobScheduler : IAgendarLembreteJobScheduler
    {
        private readonly IAgendarLembreteUseCase _agendarLembreteUseCase;

        public AgendarLembreteJobScheduler
        (
            IAgendarLembreteUseCase agendarLembreteUseCase
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
