using Application.Interfaces.Messaging;
using Infra.Mensageria.RabbitMQ.Channels;
using Infra.Mensageria.RabbitMQ.Topology;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Infra.Mensageria.RabbitMQ.Consumidores;

public class NotificarEmailConsumer : IMessageConsumer
{
    private readonly IRabbitChannelFactory _channelFactory;
    private readonly IRabbitTopologyInitializer _topology;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<NotificarEmailConsumer> _logger;

    public NotificarEmailConsumer(
        IRabbitChannelFactory channelFactory,
        IRabbitTopologyInitializer topology,
        IServiceScopeFactory scopeFactory,
        ILogger<NotificarEmailConsumer> logger)
    {
        _channelFactory = channelFactory;
        _topology = topology;
        _scopeFactory = scopeFactory;
        _logger = logger;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var channel = await _channelFactory.CreateChannelAsync();
        await _topology.InitializeAsync(channel);

        var consumer = new AsyncEventingBasicConsumer(channel);

        consumer.ReceivedAsync += async (_, args) =>
        {
            try
            {
                using var scope = _scopeFactory.CreateScope();

                var dispatcher = scope.ServiceProvider.GetRequiredService<IMessageDispatcher>();

                var json = Encoding.UTF8.GetString(args.Body.ToArray());

                await dispatcher.DispatchAsync(json);
                await channel.BasicAckAsync(args.DeliveryTag, false);
            }
            catch (Exception ex)
            {
                _logger.LogError("Erro ao processar mensagem: " + ex.Message + " || StackTrace: " + ex.StackTrace );
                await channel.BasicNackAsync(args.DeliveryTag, false, true);
            }
        };

        await channel.BasicConsumeAsync(
            queue: "email.queue",
            autoAck: false,
            consumer: consumer
        );
    }
}