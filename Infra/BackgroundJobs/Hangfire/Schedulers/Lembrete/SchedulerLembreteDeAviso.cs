using Application.Interfaces.Schedulers;
using Hangfire;
using Infra.BackgroundJobs.Hangfire.Jobs.Lembrete;

namespace Infra.Jobs.Hangfire.JobDeAgendamentos
{
    public class SchedulerLembreteDeAviso : IBackgroundLembreteJobScheduler
    {
        public Task AgendarLembreteDeAvisoAsync(Guid lembreteId, DateTimeOffset dataVencimento, int diasAntesDoVencimento)
        {
            var dataExecucao = dataVencimento.AddDays(-diasAntesDoVencimento);

            dataExecucao.AddMinutes(-1);

            if (dataExecucao <= DateTime.UtcNow)
                dataExecucao = DateTime.UtcNow;

            BackgroundJob.Schedule<DispararLembreteJob>(
                job => job.Execute(lembreteId),
                dataExecucao
            );

            return Task.CompletedTask;
        }
    }
}
//var ExecutarJobEm = lembrete.ToDoItem.DataVencimento.Value.Subtract(lembrete.PrazoDeAvisoAntesDoVencimento);