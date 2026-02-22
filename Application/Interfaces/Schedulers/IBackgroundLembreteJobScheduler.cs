using Domain.Entities.Lembretes;

namespace Application.Interfaces.Schedulers
{
    public interface IBackgroundLembreteJobScheduler
    {
        Task AgendarLembreteDeAvisoAsync(Guid lembreteId, DateTimeOffset dataVencimento, int diasAntesDoVencimento);
    }
}
