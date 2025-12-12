using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

public class WorkerDeNotificacoes : BackgroundService
{
    private readonly ILogger<WorkerDeNotificacoes> _logger;
    private readonly IConfiguration _config;
    private IConnection? _connection;
    private IChannel? _channel;

    public WorkerDeNotificacoes(ILogger<WorkerDeNotificacoes> logger, IConfiguration config)
    {
        _logger = logger;
        _config = config;
    }

    public override async Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Iniciando Worker de Notificações...");

        var uri = _config["RabbitMQ:Uri"];

        var factory = new ConnectionFactory
        {
            Uri = new Uri(uri)
        };

        _connection = await factory.CreateConnectionAsync(cancellationToken: cancellationToken);
        _channel = await _connection.CreateChannelAsync
                    (
                        options: null,
                        cancellationToken: cancellationToken
                    );


        await _channel.QueueDeclareAsync(
            queue: "notificacoes",
            durable: true,
            exclusive: false,
            autoDelete: false,
            cancellationToken: cancellationToken
        );

        await base.StartAsync(cancellationToken);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        if (_channel == null)
        {
            throw new InvalidOperationException("Canal RabbitMQ não foi inicializado.");
        }

        var consumer = new AsyncEventingBasicConsumer(_channel);

        consumer.ReceivedAsync += async (sender, ea) =>
        {
            try
            {
                string json = Encoding.UTF8.GetString(ea.Body.ToArray());
                _logger.LogInformation($"Mensagem recebida do RabbitMQ: {json}");

                var dados = JsonSerializer.Deserialize<Dictionary<string, object>>(json);

                // Simulação de envio de notificação (e-mail, push, SMS etc.)
                _logger.LogInformation($"Processando notificação → {json}");

                // Confirma processamento
                await _channel.BasicAckAsync(ea.DeliveryTag, multiple: false);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao processar mensagem.");
                // NACK → devolve mensagem para a fila
                await _channel.BasicNackAsync(ea.DeliveryTag, multiple: false, requeue: true);
            }
        };

        await _channel.BasicConsumeAsync(
            queue: "notificacoes",
            autoAck: false, // só dá ACK quando a mensagem for processada
            consumer: consumer,
            cancellationToken: stoppingToken
        );

        await Task.CompletedTask;
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Encerrando Worker de Notificações...");

        if (_channel != null)
            await _channel.CloseAsync(cancellationToken);

        if (_connection != null)
            await _connection.CloseAsync(cancellationToken);

        await base.StopAsync(cancellationToken);
    }
}
