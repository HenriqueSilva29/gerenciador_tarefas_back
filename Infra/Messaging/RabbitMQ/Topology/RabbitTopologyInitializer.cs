using RabbitMQ.Client;

namespace Infra.Mensageria.RabbitMQ.Topology
{
    public class RabbitTopologyInitializer : IRabbitTopologyInitializer
    {
        public async Task InitializeAsync(IChannel channel)
        {
            await channel.QueueDeclareAsync(
                queue: "notificacoes",
                durable: true,
                exclusive: false,
                autoDelete: false
            );
        }
    }
}
