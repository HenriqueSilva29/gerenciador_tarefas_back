using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Application.Interfaces.Messaging;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IEnumerable<IMessageConsumer> _consumers;

    public Worker(
              ILogger<Worker> logger,
              IEnumerable<IMessageConsumer> consumer)
    {
        _logger = logger;
        _consumers = consumer;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("startando consumidores...");

        foreach (var consumer in _consumers)
        {
            await consumer.StartAsync(stoppingToken);
        }

        await Task.Delay(Timeout.Infinite, stoppingToken);
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Encerrando Worker...");
        await base.StopAsync(cancellationToken);
    }
}
