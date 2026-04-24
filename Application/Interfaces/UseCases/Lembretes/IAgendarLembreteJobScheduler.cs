using Domain.Common.ValueObjects;

namespace Application.Interfaces.UseCases.Lembretes
{
    public interface IAgendarLembreteJobScheduler
    {
        Task ExecuteAsync(int id, UtcDateTime dataDisparo);
    }
}
