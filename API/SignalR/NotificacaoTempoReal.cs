using API.Hubs;
using Application.Dtos.Notificacoes;
using Application.Interfaces.Notificacoes;
using Microsoft.AspNetCore.SignalR;

namespace API.SignalR
{
    public class NotificacaoTempoReal : INotificacaoTempoReal
    {
        private readonly IHubContext<NotificacaoHub> _hubContext;

        public NotificacaoTempoReal(IHubContext<NotificacaoHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task NotificarUsuarioAsync(int? usuarioId, NotificacaoTempoRealResponse dto)
        {
            await _hubContext.Clients
                .User(usuarioId.ToString())
                .SendAsync("notificacaoRecebida", dto);
        }
    }
}
