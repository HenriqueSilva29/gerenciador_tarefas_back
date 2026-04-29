namespace Application.Interfaces.Messaging
{
    public interface IRabbitEventPublisher
    {
        Task PublishAsync<T>(T @event);
    }
}
