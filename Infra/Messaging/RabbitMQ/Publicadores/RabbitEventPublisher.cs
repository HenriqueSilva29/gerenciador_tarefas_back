using Application.Funcionalidades.Notificacoes.Eventos;
using Application.Funcionalidades.Tarefas.Eventos;
using Application.Interfaces.Messaging;
using Application.Messaging;
using Application.Observabilidade;
using Infra.Messaging.RabbitMQ.Channels;
using Infra.Messaging.RabbitMQ.Topology;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using Serilog.Context;
using System.Diagnostics;
using System.Text;
using System.Text.Json;

namespace Infra.Messaging.RabbitMQ.Publicadores
{
    public class RabbitEventPublisher : IRabbitEventPublisher
    {
        private readonly IRabbitChannelFactory _channelFactory;
        private readonly ICorrelationContextAccessor _correlationContextAccessor;
        private readonly ILogger<RabbitEventPublisher> _logger;

        private readonly Dictionary<Type, string> RoutingMap = new()
        {
            { typeof(TarefaCriadaEvento), RoutingKeys.TarefaCriada },
            { typeof(NotificacaoCriadaEvento), RoutingKeys.NotificacaoCriada }
        };

        public RabbitEventPublisher(
            IRabbitChannelFactory channelFactory,
            ICorrelationContextAccessor correlationContextAccessor,
            ILogger<RabbitEventPublisher> logger)
        {
            _channelFactory = channelFactory;
            _correlationContextAccessor = correlationContextAccessor;
            _logger = logger;
        }

        public async Task PublishAsync<T>(T @event)
        {
            var eventType = typeof(T);

            if (!RoutingMap.TryGetValue(eventType, out var routingKey))
                throw new InvalidOperationException(
                    $"RoutingKey nao configurada para {eventType.Name}");

            using var activity = ObservabilidadeFonte.ActivitySource.StartActivity(
                $"rabbitmq publish {eventType.Name}",
                ActivityKind.Producer);

            var correlationContext = _correlationContextAccessor.Context;

            var correlationId = correlationContext?.CorrelationId
                ?? Activity.Current?.TraceId.ToString()
                ?? Guid.NewGuid().ToString("N");

            var traceId = Activity.Current?.TraceId.ToString()
                ?? correlationContext?.TraceId
                ?? correlationId;

            activity?.SetTag("messaging.system", "rabbitmq");
            activity?.SetTag("messaging.destination", RabbitTopologyNames.EventsExchange);
            activity?.SetTag("messaging.rabbitmq.routing_key", routingKey);
            activity?.SetTag("messaging.message.type", eventType.Name);
            activity?.SetTag("correlation.id", correlationId);

            var envelope = new MessageEnvelope
            {
                Type = eventType.Name,
                CorrelationId = correlationId,
                TraceParent = Activity.Current?.Id ?? correlationContext?.TraceParent,
                TraceState = Activity.Current?.TraceStateString ?? correlationContext?.TraceState,
                CreatedAt = DateTime.UtcNow,
                Payload = JsonSerializer.Serialize(@event)
            };

            var body = Encoding.UTF8.GetBytes(
                JsonSerializer.Serialize(envelope));

            using var channel = await _channelFactory.CreateChannelAsync();

            using (LogContext.PushProperty("CorrelationId", correlationId))
            using (LogContext.PushProperty("TraceId", traceId))
            {
                _logger.LogInformation(
                    "Publicando evento RabbitMQ {Evento} com routing key {RoutingKey}",
                    eventType.Name,
                    routingKey);
            }

            await channel.BasicPublishAsync(
                exchange: RabbitTopologyNames.EventsExchange,
                routingKey: routingKey,
                mandatory: false,
                basicProperties: new BasicProperties
                {
                    Persistent = true,
                    ContentType = "application/json",
                    MessageId = Guid.NewGuid().ToString(),
                    CorrelationId = correlationId,
                    Headers = new Dictionary<string, object?>
                    {
                        ["traceparent"] = envelope.TraceParent,
                        ["tracestate"] = envelope.TraceState,
                        ["correlation-id"] = correlationId
                    }
                },
                body: body
            );
        }
    }
}
