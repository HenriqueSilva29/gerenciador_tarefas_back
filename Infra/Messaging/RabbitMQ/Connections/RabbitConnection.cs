using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace Infra.Mensageria.RabbitMQ.Connections
{
    public class RabbitConnection : IRabbitConnection
    {
        private readonly ConnectionFactory _factory;
        private IConnection? _connection;
        private readonly ILogger<RabbitConnection> _logger;

        public IConnection Connection { get; }

        public RabbitConnection(IConfiguration config, ILogger<RabbitConnection> logger)
        {
            _logger = logger;

            _factory = new ConnectionFactory
            {
                Uri = new Uri(config["RabbitMQ:Uri"]!)
            };

            var uriString = _factory.Uri;

            _logger.LogInformation("======================================");
            _logger.LogInformation("Tentando conectar no RabbitMQ...");
            _logger.LogInformation("URI configurada: {Uri}", uriString);


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
