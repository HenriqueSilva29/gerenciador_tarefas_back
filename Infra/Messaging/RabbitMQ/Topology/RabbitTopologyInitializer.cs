using Infra.Messaging.RabbitMQ.Topology;
using RabbitMQ.Client;

namespace Infra.Mensageria.RabbitMQ.Topology
{
    public class RabbitTopologyInitializer : IRabbitTopologyInitializer
    {
        readonly IEnumerable<IRabbitTopology> _topologies;
        public RabbitTopologyInitializer
        (
            IEnumerable<IRabbitTopology> topologies
        )
        {
            _topologies = topologies;
        }

        public async Task InitializeAsync(IChannel channel)
        {
            if (!_topologies.Any())
            {
                throw new Exception("Nenhuma topologia RabbitMQ registrada");
            }

            foreach (var topology in _topologies)
            {
                await topology.ConfigureAsync(channel);
            }
        }
    }
}
