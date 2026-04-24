using Domain.Common.ValueObjects;

namespace Application.Interfaces.UseCases.Lembretes
{
    public interface IAgendarLembreteUseCase
    {
        Task ExecuteAsync(int id, UtcDateTime dataDisparo); 
    }
}
