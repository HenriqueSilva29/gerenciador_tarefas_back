using RabbitMQ.Client;

namespace Infra.Messaging.RabbitMQ.Topology
{
    public interface IRabbitTopology
    {
        Task ConfigureAsync(IChannel channel);
    }
}
