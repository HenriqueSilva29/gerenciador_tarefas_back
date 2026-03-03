using Application.Interfaces.UseCases.Lembretes;

namespace Infra.BackgroundJobs.Hangfire.Jobs.Lembretes
{
    public class VerificarLembretesVencendoJob
    {
        private readonly IVerificarLembretesPertoDoVencimentoUseCase _useCase;

        public VerificarLembretesVencendoJob(
            IVerificarLembretesPertoDoVencimentoUseCase useCase)
        {
            _useCase = useCase;
        }

        public async Task Execute()
        {
            await _useCase.ExecuteAsync();
        }
    }
}