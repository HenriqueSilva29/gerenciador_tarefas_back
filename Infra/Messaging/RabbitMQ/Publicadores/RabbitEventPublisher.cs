using Application.Messaging;
using Infra.Mensageria.RabbitMQ.Channels;
using Infra.Messaging.RabbitMQ.Publicadores;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace Infra.Mensageria.RabbitMQ.Publicadores
{
    public class RabbitEventPublisher : IRabbitEventPublisher
    {
        private readonly IRabbitChannelFactory _channelFactory;

        public RabbitEventPublisher(IRabbitChannelFactory channelFactory)
        {
            _channelFactory = channelFactory;
        }

        public async Task PublishAsync<T>(string eventType, T data)
        {
            var envelope = new MessageEnvelope
            {
                Type = eventType,
                CorrelationId = Guid.NewGuid().ToString(),
                CreatedAt = DateTime.UtcNow,
                Payload = JsonSerializer.Serialize(data)
            };

            var json = JsonSerializer.Serialize(envelope);
            var body = Encoding.UTF8.GetBytes(json);

            using var channel = await _channelFactory.CreateChannelAsync();

            await channel.BasicPublishAsync(
                exchange: "app.events",
                routingKey: eventType,
                body: body
            );
        }

    }
}
