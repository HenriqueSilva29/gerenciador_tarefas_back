using RabbitMQ.Client;

namespace Infra.Messaging.RabbitMQ.Connections
{
    public interface IRabbitConnection : IDisposable
    {
        Task<IConnection> GetConnectionAsync();
        IConnection Connection { get; }
    }
}
