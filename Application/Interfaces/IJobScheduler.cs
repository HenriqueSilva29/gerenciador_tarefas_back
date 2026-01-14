using Domain.Entities.Lembretes;

namespace Application.Interfaces
{
    public interface IJobScheduler
    {
        public void AgendarLembrete(Lembrete entity);
    }
}
