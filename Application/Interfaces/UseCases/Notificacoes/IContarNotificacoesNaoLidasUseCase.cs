namespace Application.Interfaces.UseCases.Notificacoes
{
    public interface IContarNotificacoesNaoLidasUseCase
    {
        Task<int> ExecuteAsync();
    }
}
