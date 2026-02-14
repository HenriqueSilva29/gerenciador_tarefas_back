using Application.UseCase.Lembrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.BackgroundJobs.Hangfire.Jobs.Lembrete
{
    public class DispararLembreteJob
    {
        private readonly DispararLembreteUseCase _publicarNotificacaoDeLembreteUseCase;

        public DispararLembreteJob(DispararLembreteUseCase useCase)
        {
            _publicarNotificacaoDeLembreteUseCase = useCase;
        }

        public async Task Execute(Guid lembreteId)
        {
            await _publicarNotificacaoDeLembreteUseCase.ExecuteAsync(lembreteId);
        }
    }
}
