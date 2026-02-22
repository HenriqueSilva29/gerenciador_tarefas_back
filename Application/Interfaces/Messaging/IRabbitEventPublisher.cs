namespace Infra.Messaging.RabbitMQ.Publicadores
{
    public interface IRabbitEventPublisher
    {
        Task PublishAsync<T>(string eventType, T data);
    }
}
