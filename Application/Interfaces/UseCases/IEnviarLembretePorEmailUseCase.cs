namespace Application.Interfaces.UseCases
{
    public interface IEnviarLembretePorEmailUseCase
    {
        Task ExecuteAsync(int lembreteId);
    }
}
