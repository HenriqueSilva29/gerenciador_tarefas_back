using Microsoft.EntityFrameworkCore.Metadata;
using RabbitMQ.Client;

namespace Infra.Mensageria.RabbitMQ
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
            var channel = await connection.CreateChannelAsync();

            await channel.QueueDeclareAsync(
                queue: "notificacoes",
                durable: true,
                exclusive: false,
                autoDelete: false
            );

            return channel;
        }
    }
}
