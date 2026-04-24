using RabbitMQ.Client;

namespace Infra.Messaging.RabbitMQ.Topology
{
    public interface IRabbitTopologyInitializer
    {
        Task InitializeAsync(IChannel channel);
    }
}
