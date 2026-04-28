using Application.Dtos.Notificacoes;

namespace Application.Interfaces.Notificacoes
{
    public interface INotificacaoTempoReal
    {
        Task NotificarUsuarioAsync(int? usuarioId, NotificacaoTempoRealResponse dto);
    }
}
