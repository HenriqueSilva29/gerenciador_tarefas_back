using Infra.Mensageria.RabbitMQ.Topology;
using Infra.Messaging.RabbitMQ;
using Infra.Messaging.RabbitMQ.Consumidores.Tarefas;
using Infra.Messaging.RabbitMQ.Topology;
using RabbitMQ.Client;

namespace Infra.Messaging.RabbitMQ.Topology.Topologies.Tarefas
{
    public class GerarLembreteTopology : IRabbitTopology
    {
        public Task ConfigureAsync(IChannel channel)
            => RabbitTopologyHelper.DeclareConsumerTopologyAsync(
                channel,
                GerarLembreteConsumer.Queue,
                RoutingKeys.TarefaCriada);
    }
}
