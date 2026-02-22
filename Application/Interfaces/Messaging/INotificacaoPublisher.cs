using Application.Dtos.LembreteDtos;
using Domain.Entities.Lembretes;

namespace Application.Interfaces.Messaging
{
    public interface INotificacaoPublisher
    {
        Task PublicarAsync(Lembrete entity);
    }
}
