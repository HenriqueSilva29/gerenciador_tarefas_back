using Application.Interfaces;
using Domain.Entities.Lembretes;
using Hangfire;
using Infra.Jobs.Hangfire.JobDeLembretes;

namespace Infra.Jobs.Hangfire.JobDeAgendamentos
{
    public class JobScheduler : IJobScheduler
    {
        public void AgendarLembrete(Lembrete lembrete)
        {
            BackgroundJob.Schedule<IJobDeLembrete>(
                job => job.ExecutarAsync(lembrete.Id)
                       ,lembrete.DataDeAgendamento);
        }
    }
}
