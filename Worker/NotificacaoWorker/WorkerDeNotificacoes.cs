using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using Infra.Mensageria.RabbitMQ.Consumidores;
using Application.Services.ServLembretes;
using Microsoft.Extensions.DependencyInjection;

public class WorkerDeNotificacoes : BackgroundService
{
    private readonly ILogger<WorkerDeNotificacoes> _logger;
    private readonly IConfiguration _config;
    private readonly IServiceScopeFactory _scopeFactory;
    private IConnection? _connection;
    private IChannel? _channel;


    public WorkerDeNotificacoes(
          ILogger<WorkerDeNotificacoes> logger,
          IConfiguration config,
          IServiceScopeFactory scopeFactory)
    {
        _logger = logger;
        _config = config;
        _scopeFactory = scopeFactory;
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

        await base.StartAsync(cancellationToken);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        if(_channel == null)
            throw new InvalidOperationException("Canal não inicializado");

        var consumidor = new ConsumidorDeMensagens(_channel, _scopeFactory);
        await consumidor.IniciarAsync();

        await Task.Delay(Timeout.Infinite, stoppingToken);
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
