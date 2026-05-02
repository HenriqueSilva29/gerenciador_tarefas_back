using Application.Interfaces.Messaging;
using Application.Messaging;
using Application.Observabilidade;
using Infra.Messaging.RabbitMQ.Channels;
using Infra.Messaging.RabbitMQ.Topology;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Serilog.Context;
using System.Diagnostics;
using System.Text;
using System.Text.Json;

namespace Infra.Messaging.RabbitMQ.Consumidores
{
    public abstract class RabbitMessageConsumerBase : IMessageConsumer
    {
        private readonly IRabbitChannelFactory _channelFactory;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<RabbitMessageConsumerBase> _logger;

        protected RabbitMessageConsumerBase(
            IRabbitChannelFactory channelFactory,
            IServiceScopeFactory scopeFactory,
            ILogger<RabbitMessageConsumerBase> logger)
        {
            _channelFactory = channelFactory;
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        protected abstract string QueueName { get; }
        protected abstract string RoutingKey { get; }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var channel = await _channelFactory.CreateChannelAsync();
            var consumer = new AsyncEventingBasicConsumer(channel);

            consumer.ReceivedAsync += async (_, args) =>
            {
                MessageEnvelope? envelope = null;
                var json = string.Empty;

                try
                {
                    var retryCount = HelperConsumer.GetRetryCount(args);
                    json = Encoding.UTF8.GetString(args.Body.ToArray());
                    envelope = JsonSerializer.Deserialize<MessageEnvelope>(json);
                    var correlationId = envelope?.CorrelationId ?? Guid.NewGuid().ToString("N");

                    if (retryCount >= 3)
                    {
                        await channel.BasicPublishAsync(
                            exchange: RabbitTopologyNames.DlqExchange,
                            routingKey: RoutingKey,
                            body: args.Body,
                            cancellationToken: cancellationToken);

                        await channel.BasicAckAsync(args.DeliveryTag, false, cancellationToken);
                        return;
                    }

                    using var scope = _scopeFactory.CreateScope();
                    ActivityContext parentContext = default;
                    var hasParent = envelope is not null &&
                        ActivityContext.TryParse(
                            envelope.TraceParent,
                            envelope.TraceState,
                            out parentContext);

                    var traceId = hasParent
                        ? parentContext.TraceId.ToString()
                        : correlationId;

                    using var activity = hasParent
                        ? ObservabilidadeFonte.ActivitySource.StartActivity(
                            $"rabbitmq consume {envelope?.Type ?? QueueName}",
                            ActivityKind.Consumer,
                            parentContext)
                        : ObservabilidadeFonte.ActivitySource.StartActivity(
                            $"rabbitmq consume {envelope?.Type ?? QueueName}",
                            ActivityKind.Consumer);

                    activity?.SetTag("messaging.system", "rabbitmq");
                    activity?.SetTag("messaging.destination", QueueName);
                    activity?.SetTag("messaging.rabbitmq.routing_key", RoutingKey);
                    activity?.SetTag("messaging.message.type", envelope?.Type);
                    activity?.SetTag("correlation.id", correlationId);

                    traceId = Activity.Current?.TraceId.ToString() ?? traceId;

                    var correlationContextAccessor = scope.ServiceProvider.GetRequiredService<ICorrelationContextAccessor>();
                    correlationContextAccessor.Context = new CorrelationContext
                    {
                        CorrelationId = correlationId,
                        TraceId = traceId,
                        TraceParent = envelope?.TraceParent,
                        TraceState = envelope?.TraceState
                    };

                    var dispatcher = scope.ServiceProvider.GetRequiredService<IMessageDispatcher>();

                    using (_logger.BeginScope(new Dictionary<string, object>
                    {
                        ["CorrelationId"] = correlationId,
                        ["TraceId"] = traceId
                    }))
                    using (LogContext.PushProperty("CorrelationId", correlationId))
                    using (LogContext.PushProperty("TraceId", traceId))
                    {
                        _logger.LogInformation(
                            "Consumindo mensagem RabbitMQ {Evento} da fila {Fila}",
                            envelope?.Type,
                            QueueName);

                        await dispatcher.DispatchAsync(json);
                    }

                    await channel.BasicAckAsync(args.DeliveryTag, false, cancellationToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(
                        ex,
                        "Erro ao consumir mensagem RabbitMQ {Evento} da fila {Fila}",
                        envelope?.Type,
                        QueueName);

                    await channel.BasicNackAsync(args.DeliveryTag, false, false, cancellationToken);
                }
            };

            await channel.BasicConsumeAsync(
                queue: QueueName,
                autoAck: false,
                consumer: consumer,
                cancellationToken: cancellationToken);
        }
    }
}
