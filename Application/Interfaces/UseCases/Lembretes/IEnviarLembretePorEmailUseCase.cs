namespace Application.Interfaces.UseCases.Lembretes
{
    public interface IEnviarLembretePorEmailUseCase
    {
        Task ExecuteAsync(int idtarefa);
    }
}
