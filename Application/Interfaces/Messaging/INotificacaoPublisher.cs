using Application.Dtos.LembreteDtos;
using Domain.Entities;

namespace Application.Interfaces.Messaging
{
    public interface INotificacaoPublisher
    {
        Task PublicarAsync(Lembrete entity);
    }
}
