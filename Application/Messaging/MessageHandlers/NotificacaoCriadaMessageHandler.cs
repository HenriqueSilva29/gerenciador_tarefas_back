using Application.Funcionalidades.Notificacoes.Dtos;
using Application.Funcionalidades.Notificacoes.Eventos;
using Application.Interfaces.Messaging;
using Application.Funcionalidades.Notificacoes.Contratos.TempoReal;

namespace Application.Messaging.MessageHandlers
{
    public class NotificacaoCriadaMessageHandler : IMessageHandler<NotificacaoCriadaEvento>
    {
        private readonly INotificarUsuarioCasoDeUso _notificarUsuarioUseCase;

        public NotificacaoCriadaMessageHandler(INotificarUsuarioCasoDeUso notificarUsuarioUseCase)
        {
            _notificarUsuarioUseCase = notificarUsuarioUseCase;
        }

        public Task HandleAsync(NotificacaoCriadaEvento message)
            => _notificarUsuarioUseCase.ExecuteAsync(message);
    }
}
