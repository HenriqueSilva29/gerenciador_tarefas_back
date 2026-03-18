using Infra.Mensageria.RabbitMQ.Channels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Infra.Messaging.RabbitMQ.Topology
{
    public class RabbitInitializerHostedService : IHostedService
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public RabbitInitializerHostedService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Inicializando RabbitMQ...");

            var scope = _scopeFactory.CreateScope();
            var initializer = scope.ServiceProvider.GetService<IRabbitTopologyInitializer>();
            var channelFactory = scope.ServiceProvider.GetService<IRabbitChannelFactory>();

            var channel = await channelFactory.CreateChannelAsync();

            await initializer.InitializeAsync(channel);

            Console.WriteLine("RabbitMQ inicializado");
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
