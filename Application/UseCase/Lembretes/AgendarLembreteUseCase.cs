using Application.Interfaces.UseCases.Lembretes;
using Domain.Common.ValueObjects;
using Hangfire;

namespace Application.UseCase.Lembretes
{
    public class AgendarLembreteUseCase : IAgendarLembreteUseCase
    {
        private readonly IDispararLembreteUseCase _dispararLembreteUseCase;
        public AgendarLembreteUseCase(IDispararLembreteUseCase dispararLembreteUseCase)
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
