using Application.Events.Tarefas;
using Application.Messaging;
using Infra.Mensageria.RabbitMQ.Channels;
using Infra.Messaging.RabbitMQ;
using Infra.Messaging.RabbitMQ.Publicadores;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace Infra.Mensageria.RabbitMQ.Publicadores
{
    public class RabbitEventPublisher : IRabbitEventPublisher
    {
        private readonly IRabbitChannelFactory _channelFactory;

        private readonly Dictionary<Type, string> RoutingMap = new()
        {
            {typeof(LembreteVencimentoAtingidoEvent),RoutingKeys.LembreteVencimentoAtingidoV1},
            {typeof(TarefaCriadaEvent),RoutingKeys.TarefaCriada}
        };

        public RabbitEventPublisher(IRabbitChannelFactory channelFactory)
        {
            _channelFactory = channelFactory;
        }

        public async Task PublishAsync<T>(T @event)
        {
            var eventType = typeof(T);

            if (!RoutingMap.TryGetValue(eventType, out var routingKey))
                throw new InvalidOperationException(
                    $"RoutingKey não configurada para {eventType.Name}");


            var envelope = new MessageEnvelope
            {
                Type = eventType.Name,
                CorrelationId = Guid.NewGuid().ToString(),
                CreatedAt = DateTime.UtcNow,
                Payload = JsonSerializer.Serialize(@event)
            };

            var body = Encoding.UTF8.GetBytes(
                JsonSerializer.Serialize(envelope));

            using var channel = await _channelFactory.CreateChannelAsync();

            await channel.BasicPublishAsync(
                exchange: "app.events",
                routingKey: routingKey,
                mandatory: false,
                basicProperties: new BasicProperties
                {
                    Persistent = true,
                    ContentType = "application/json",
                    MessageId = Guid.NewGuid().ToString()
                },
                body: body
            );

        }
    }
}
