namespace Application.Interfaces.UseCases.Notificacoes
{
    public interface IExcluirNotificacaoUseCase
    {
        Task ExecuteAsync(int id);
    }
}
