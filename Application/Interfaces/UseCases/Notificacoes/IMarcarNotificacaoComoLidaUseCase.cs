namespace Application.Interfaces.UseCases.Notificacoes
{
    public interface IMarcarNotificacaoComoLidaUseCase
    {
        Task ExecuteAsync(int id);
    }
}
