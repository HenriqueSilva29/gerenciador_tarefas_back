using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;

namespace Infra.Mensageria.RabbitMQ
{
    public class RabbitConnection : IRabbitConnection
    {
        private readonly ConnectionFactory _factory;
        private IConnection? _connection;

        public RabbitConnection(IConfiguration config)
        {
            _factory = new ConnectionFactory
            {
                Uri = new Uri(config["RabbitMQ:Uri"]!)
            };
        }

        public async Task<IConnection> GetConnectionAsync()
        {
            if (_connection is { IsOpen: true })
                return _connection;

            _connection = await _factory.CreateConnectionAsync();
            return _connection;
        }

        public void Dispose()
        {
            if (_connection?.IsOpen == true)
                _connection.Dispose();
        }
    }
}
    