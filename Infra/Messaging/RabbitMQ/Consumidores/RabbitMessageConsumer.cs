using Application.Dtos.LembreteDtos;
using Application.Interfaces.Messaging;
using Infra.Mensageria.RabbitMQ.Channels;
using Infra.Mensageria.RabbitMQ.Topology;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
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
    private readonly ILogger<RabbitMessageConsumer> _logger;

    public RabbitMessageConsumer(
        IRabbitChannelFactory channelFactory,
        IRabbitTopologyInitializer topology,
        IServiceScopeFactory scopeFactory,
        ILogger<RabbitMessageConsumer> logger)
    {
        _channelFactory = channelFactory;
        _topology = topology;
        _scopeFactory = scopeFactory;
        _logger = logger;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("BasicConsume registrado");

            var channel = await _channelFactory.CreateChannelAsync();
            await _topology.InitializeAsync(channel);

            var consumer = new AsyncEventingBasicConsumer(channel);

            consumer.ReceivedAsync += async (_, args) =>
            {

                try
                {
                    _logger.LogInformation("RECEBI UMA MENSAGEM");

                    using var scope = _scopeFactory.CreateScope();

                    _logger.LogInformation("1");
                    var handler = scope.ServiceProvider.GetRequiredService<IMessageHandler<LembreteMensagemDto>>();
                    _logger.LogInformation("2");

                    var json = Encoding.UTF8.GetString(args.Body.ToArray());

                    _logger.LogInformation("3");
                    var message = new LembreteMensagemDto();
                    _logger.LogInformation("4");
                    try
                    {
                        _logger.LogInformation("Mensagemmm");
                        message = JsonSerializer.Deserialize<LembreteMensagemDto>(json);
                    }
                    catch
                    {
                        _logger.LogInformation("Falhou");
                        await channel.BasicNackAsync(args.DeliveryTag, false, false);
                        return;
                    }

                    if (message == null)
                    {
                        _logger.LogInformation("Message null");
                        await channel.BasicNackAsync(args.DeliveryTag, false, true);
                        return;
                    }
                    _logger.LogInformation("Chegou aqui?");
                    try
                    {
                        _logger.LogInformation("Antes do HandleAsync");

                        await handler.HandleAsync(message, cancellationToken);
                        await Task.Delay(1000);
                        _logger.LogInformation("Depois do HandleAsync");
                        await channel.BasicAckAsync(args.DeliveryTag, false);
                        _logger.LogInformation("ACK enviado");
                    }
                    catch
                    {
                        _logger.LogInformation("Falha");
                        await channel.BasicNackAsync(args.DeliveryTag, false, true);
                    }

                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "EXCECAO NO CALLBACK RABBIT");
                    await channel.BasicNackAsync(args.DeliveryTag, false, true);
                }

            };

            await channel.BasicConsumeAsync(
                queue: "notificacoes",
                autoAck: false,
                consumer: consumer
            );
        }
        catch(Exception e)
        {
            _logger.LogInformation($"Error: {e.StackTrace}");
        }
     }
}