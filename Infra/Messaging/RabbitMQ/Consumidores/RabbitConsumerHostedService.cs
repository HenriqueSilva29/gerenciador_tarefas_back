using Application.Interfaces.Messaging;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Infra.Messaging.RabbitMQ.Consumidores
{
    public class RabbitConsumerHostedService : BackgroundService
    {
        private readonly ILogger<RabbitConsumerHostedService> _logger;
        private readonly IEnumerable<IMessageConsumer> _consumers;

        public RabbitConsumerHostedService(
            ILogger<RabbitConsumerHostedService> logger,
            IEnumerable<IMessageConsumer> consumers)
        {
            _logger = logger;
            _consumers = consumers;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Iniciando consumidores RabbitMQ...");

            foreach (var consumer in _consumers)
            {
                await consumer.StartAsync(stoppingToken);
            }

            await Task.Delay(Timeout.Infinite, stoppingToken);
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Encerrando consumidores RabbitMQ...");
            await base.StopAsync(cancellationToken);
        }
    }
}
