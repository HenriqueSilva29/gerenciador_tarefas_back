using API.Hubs;
using Application.Funcionalidades.Notificacoes.Dtos;
using Application.Funcionalidades.Notificacoes.Contratos.TempoReal;
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

        public async Task NotificarUsuarioAsync(int? usuarioId, NotificacaoTempoRealResposta dto)
        {
            await _hubContext.Clients
                .User(usuarioId.ToString())
                .SendAsync("notificacaoRecebida", dto);
        }
    }
}
