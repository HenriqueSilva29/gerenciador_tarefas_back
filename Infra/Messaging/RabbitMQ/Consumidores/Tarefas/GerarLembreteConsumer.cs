using Application.Interfaces.Messaging;
using Infra.Mensageria.RabbitMQ.Channels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Infra.Messaging.RabbitMQ.Consumidores.Tarefas
{
    public class GerarLembreteConsumer : IMessageConsumer
    {
        private readonly IRabbitChannelFactory _channelFactory;
        private readonly IServiceScopeFactory _scopeFactory;

        public GerarLembreteConsumer
        (
            IRabbitChannelFactory channelFactory,
            IServiceScopeFactory scopeFactory)
        { 
            _channelFactory = channelFactory;
            _scopeFactory = scopeFactory;
        }
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var channel = await _channelFactory.CreateChannelAsync();

            var consumer = new AsyncEventingBasicConsumer(channel);

            consumer.ReceivedAsync += async(_, args) => {
                try
                {
                    var retryCount = HelperConsumer.GetRetryCount(args);

                    if (retryCount >= 3)
                    {
                        await channel.BasicPublishAsync(
                            exchange: "app.events.dlq",
                            routingKey: RoutingKeys.TarefaCriada,
                            body: args.Body);


                        await channel.BasicAckAsync(args.DeliveryTag, false);
                        return;
                    }

                    using var scope = _scopeFactory.CreateScope();

                    var dispatcher = scope.ServiceProvider.GetRequiredService<IMessageDispatcher>();

                    var json = Encoding.UTF8.GetString(args.Body.ToArray());

                    await dispatcher.DispatchAsync(json);
                    await channel.BasicAckAsync(args.DeliveryTag, false);

                }
                catch (Exception ex)
                {
                    await channel.BasicNackAsync(args.DeliveryTag, false, false);
                }
            };

            await channel.BasicConsumeAsync
            (
                queue: "tarefa.criada.gerar-lembrete.queue",
                autoAck: false,
                consumer: consumer
            );

        }
    }
}
