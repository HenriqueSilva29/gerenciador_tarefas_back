using Infra.Messaging.RabbitMQ.Connections;
using Microsoft.EntityFrameworkCore.Metadata;
using RabbitMQ.Client;

namespace Infra.Messaging.RabbitMQ.Channels
{
    public class RabbitChannelFactory : IRabbitChannelFactory
    {
        private readonly IRabbitConnection _connection;

        public RabbitChannelFactory(IRabbitConnection connection)
        {
            _connection = connection;
        }

        public async Task<IChannel> CreateChannelAsync()
        {
            var connection = await _connection.GetConnectionAsync();
            return await connection.CreateChannelAsync();
        }
    }
}
