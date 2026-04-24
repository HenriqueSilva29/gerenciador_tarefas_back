using RabbitMQ.Client;

namespace Infra.Mensageria.RabbitMQ.Topology
{
    public interface IRabbitTopology
    {
        Task ConfigureAsync(IChannel channel);
    }
}
