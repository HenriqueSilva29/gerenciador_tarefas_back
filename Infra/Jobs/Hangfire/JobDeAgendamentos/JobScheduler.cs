using Application.Interfaces;
using Domain.Common.ValueObjects;
using Domain.Entities.Lembretes;
using Hangfire;
using Infra.Jobs.Hangfire.JobDeLembretes;
using Microsoft.Extensions.Logging;

namespace Infra.Jobs.Hangfire.JobDeAgendamentos
{
    public class JobScheduler : IJobScheduler
    {
        private readonly ILogger<JobScheduler> _logger;
        public JobScheduler(ILogger<JobScheduler> logger)
        {
            _logger = logger;
        }
        public void AgendarLembrete(Lembrete lembrete)
        {
            var ExecutarJobEm = lembrete.ToDoItem.DataVencimento.Value.Subtract(lembrete.PrazoDeAvisoAntesDoVencimento);

             BackgroundJob.Schedule<IJobDeLembrete>(
                job => job.ExecutarAsync(lembrete.Id)
                       , ExecutarJobEm);
        }

    }
}
