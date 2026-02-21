using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Application.Interfaces.Messaging;

public class WorkerDeNotificacoes : BackgroundService
{
    private readonly ILogger<WorkerDeNotificacoes> _logger;
    private readonly IMessageConsumer _consumer;

    public WorkerDeNotificacoes(
              ILogger<WorkerDeNotificacoes> logger,
               IMessageConsumer consumer)
    {
        _logger = logger;
        _consumer = consumer;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Iniciando Worker de Notificações...");
        await _consumer.StartAsync(stoppingToken);

        await Task.Delay(Timeout.Infinite, stoppingToken);
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Encerrando Worker de Notificações...");
        await base.StopAsync(cancellationToken);
    }
}
