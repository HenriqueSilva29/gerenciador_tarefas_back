using Infra.Messaging.RabbitMQ.Topology;
using Infra.Messaging.RabbitMQ;
using Infra.Messaging.RabbitMQ.Consumidores.Notificacoes;
using RabbitMQ.Client;

namespace Infra.Messaging.RabbitMQ.Topology.Topologies.Notificacoes
{
    public class NotificacaoCriadaTopology : IRabbitTopology
    {
        public Task ConfigureAsync(IChannel channel)
            => RabbitTopologyHelper.DeclareConsumerTopologyAsync(
                channel,
                NotificacaoCriadaConsumer.Queue,
                RoutingKeys.NotificacaoCriada);
    }
}
