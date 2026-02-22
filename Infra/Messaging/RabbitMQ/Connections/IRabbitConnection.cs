using RabbitMQ.Client;

namespace Infra.Mensageria.RabbitMQ.Connections
{
    public interface IRabbitConnection : IDisposable
    {
        Task<IConnection> GetConnectionAsync();
        IConnection Connection { get; }
    }
}
