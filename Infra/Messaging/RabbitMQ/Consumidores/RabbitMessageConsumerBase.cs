using Application.Interfaces.Messaging;
using Infra.Messaging.RabbitMQ.Channels;
using Infra.Messaging.RabbitMQ.Topology;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Infra.Messaging.RabbitMQ.Consumidores
{
    public abstract class RabbitMessageConsumerBase : IMessageConsumer
    {
        private readonly IRabbitChannelFactory _channelFactory;
        private readonly IServiceScopeFactory _scopeFactory;

        protected RabbitMessageConsumerBase(
            IRabbitChannelFactory channelFactory,
            IServiceScopeFactory scopeFactory)
        {
            _channelFactory = channelFactory;
            _scopeFactory = scopeFactory;
        }

        protected abstract string QueueName { get; }
        protected abstract string RoutingKey { get; }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var channel = await _channelFactory.CreateChannelAsync();
            var consumer = new AsyncEventingBasicConsumer(channel);

            consumer.ReceivedAsync += async (_, args) =>
            {
                try
                {
                    var retryCount = HelperConsumer.GetRetryCount(args);

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
                    var dispatcher = scope.ServiceProvider.GetRequiredService<IMessageDispatcher>();
                    var json = Encoding.UTF8.GetString(args.Body.ToArray());

                    await dispatcher.DispatchAsync(json);
                    await channel.BasicAckAsync(args.DeliveryTag, false, cancellationToken);
                }
                catch
                {
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
