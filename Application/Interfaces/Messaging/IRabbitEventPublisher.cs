namespace Infra.Messaging.RabbitMQ.Publicadores
{
    public interface IRabbitEventPublisher
    {
        Task PublishAsync<T>(T @event);
    }
}
