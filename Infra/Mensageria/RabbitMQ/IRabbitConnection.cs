using RabbitMQ.Client;

namespace Infra.Mensageria.RabbitMQ
{
    public interface IRabbitConnection : IDisposable
    {
        Task<IConnection> GetConnectionAsync();
    }
}
