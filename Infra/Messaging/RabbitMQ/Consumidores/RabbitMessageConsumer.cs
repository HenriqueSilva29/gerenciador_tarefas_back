using Application.Dtos.LembreteDtos;
using Application.Interfaces.Messaging;
using Infra.Mensageria.RabbitMQ.Channels;
using Infra.Mensageria.RabbitMQ.Topology;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace Infra.Mensageria.RabbitMQ.Consumidores;

public class RabbitMessageConsumer : IMessageConsumer
{
    private readonly IRabbitChannelFactory _channelFactory;
    private readonly IRabbitTopologyInitializer _topology;
    private readonly IServiceScopeFactory _scopeFactory;

    public RabbitMessageConsumer(
        IRabbitChannelFactory channelFactory,
        IRabbitTopologyInitializer topology,
        IServiceScopeFactory scopeFactory)
    {
        _channelFactory = channelFactory;
        _topology = topology;
        _scopeFactory = scopeFactory;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var channel = await _channelFactory.CreateChannelAsync();
        await _topology.InitializeAsync(channel);

        var consumer = new AsyncEventingBasicConsumer(channel);

        consumer.ReceivedAsync += async (_, args) =>
        {
            using var scope = _scopeFactory.CreateScope();

            var handler = scope.ServiceProvider
                .GetRequiredService<IMessageHandler<LembreteMensagemDto>>();

            var json = Encoding.UTF8.GetString(args.Body.ToArray());

            var message = new LembreteMensagemDto();

            try
            {
                message = JsonSerializer.Deserialize<LembreteMensagemDto>(json);
            }
            catch
            {
                await channel.BasicNackAsync(args.DeliveryTag, false, false);
                return;
            }

            if (message == null)
            {
                await channel.BasicNackAsync(args.DeliveryTag, false, true);
                return;
            }

            try
            {
                await handler.HandleAsync(message, cancellationToken);
                await channel.BasicAckAsync(args.DeliveryTag, false);
            }
            catch
            {
                await channel.BasicNackAsync(args.DeliveryTag, false, true);
            }
        };

        await channel.BasicConsumeAsync(
            queue: "notificacoes",
            autoAck: false,
            consumer: consumer
        );
    }
}