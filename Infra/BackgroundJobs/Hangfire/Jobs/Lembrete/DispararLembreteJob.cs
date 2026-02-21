using Application.Interfaces.UseCases;
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
        private readonly IDispararLembreteUseCase _dispararLembreteUseCase;

        public DispararLembreteJob(IDispararLembreteUseCase useCase)
        {
            _dispararLembreteUseCase = useCase;
        }

        public async Task Execute(Guid lembreteId)
        {
            await _dispararLembreteUseCase.ExecuteAsync(lembreteId);
        }
    }
}
