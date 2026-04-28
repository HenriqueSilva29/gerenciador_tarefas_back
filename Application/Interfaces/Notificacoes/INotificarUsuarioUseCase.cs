using Application.Events.Notificacoes;

namespace Application.Interfaces.Notificacoes
{
    public interface INotificarUsuarioUseCase
    {
        Task ExecuteAsync(NotificacaoCriadaEvent message);
    }
}
