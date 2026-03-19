using Application.Interfaces.Messaging;
using Infra.Mensageria.RabbitMQ.Channels;
using Infra.Mensageria.RabbitMQ.Topology;
using Infra.Messaging.RabbitMQ;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Infra.Messaging.RabbitMQ.Consumidores.Lembretes;

public class EnviarLembreteVencidoPorEmailConsumer : IMessageConsumer
{
    private readonly IRabbitChannelFactory _channelFactory;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<EnviarLembreteVencidoPorEmailConsumer> _logger;

    public EnviarLembreteVencidoPorEmailConsumer(
        IRabbitChannelFactory channelFactory,
        IServiceScopeFactory scopeFactory,
        ILogger<EnviarLembreteVencidoPorEmailConsumer> logger)
    {
        _channelFactory = channelFactory;
        _scopeFactory = scopeFactory;
        _logger = logger;
    }

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
                        exchange: "app.events.dlq",
                        routingKey: RoutingKeys.LembreteVencimentoAtingidoV1,
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
                _logger.LogError("Erro ao processar mensagem: " + ex.Message + " || StackTrace: " + ex.StackTrace);
                await channel.BasicNackAsync(args.DeliveryTag, false, false);
            }
        };

        await channel.BasicConsumeAsync(
            queue: "notification.email.lembrete-vencimento.queue",
            autoAck: false,
            consumer: consumer
        );
    }
}