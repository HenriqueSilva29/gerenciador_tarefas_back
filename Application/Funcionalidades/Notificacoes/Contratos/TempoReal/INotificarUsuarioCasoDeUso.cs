using Application.Funcionalidades.Notificacoes.Eventos;

namespace Application.Funcionalidades.Notificacoes.Contratos.TempoReal
{
    public interface INotificarUsuarioCasoDeUso
    {
        Task ExecuteAsync(NotificacaoCriadaEvento message);
    }
}

