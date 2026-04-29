using Application.Funcionalidades.Notificacoes.Dtos;

namespace Application.Funcionalidades.Notificacoes.Contratos.TempoReal
{
    public interface INotificacaoTempoReal
    {
        Task NotificarUsuarioAsync(int? usuarioId, NotificacaoTempoRealResposta dto);
    }
}

