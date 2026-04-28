using Application.Dtos.Notificacoes;
using Application.Events.Notificacoes;
using Application.Interfaces.Messaging;
using Application.Interfaces.Notificacoes;

namespace Application.Messaging.MessageHandlers
{
    public class NotificacaoCriadaMessageHandler : IMessageHandler<NotificacaoCriadaEvent>
    {
        private readonly INotificarUsuarioUseCase _notificarUsuarioUseCase;

        public NotificacaoCriadaMessageHandler(INotificarUsuarioUseCase notificarUsuarioUseCase)
        {
            _notificarUsuarioUseCase = notificarUsuarioUseCase;
        }

        public Task HandleAsync(NotificacaoCriadaEvent message)
            => _notificarUsuarioUseCase.ExecuteAsync(message);
    }
}
