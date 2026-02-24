

using Application.Interfaces.UseCases;

namespace Infra.BackgroundJobs.Hangfire.Jobs.Lembretes
{
    public class VerificarLembretesVencendoJob
    {
        private readonly IVerificarLembretesVencendoUseCase _useCase;

        public VerificarLembretesVencendoJob(
            IVerificarLembretesVencendoUseCase useCase)
        {
            _useCase = useCase;
        }

        public async Task Execute()
        {
            await _useCase.ExecuteAsync();
        }
    }
}