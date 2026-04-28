using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace API.Hubs
{

    [Authorize]
    public class NotificacaoHub : Hub
    {
        public override async Task OnConnectedAsync()
        {

        }
    }
}
