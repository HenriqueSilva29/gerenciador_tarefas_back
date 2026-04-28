using Infra.Mensageria.RabbitMQ.Channels;
using Infra.Messaging.RabbitMQ;
using Infra.Messaging.RabbitMQ.Consumidores;
using Microsoft.Extensions.DependencyInjection;

namespace Infra.Messaging.RabbitMQ.Consumidores.Tarefas
{
    public class GerarLembreteConsumer : RabbitMessageConsumerBase
    {
        public const string Queue = "tarefa.criada.gerar-lembrete.queue";

        public GerarLembreteConsumer(
            IRabbitChannelFactory channelFactory,
            IServiceScopeFactory scopeFactory)
            : base(channelFactory, scopeFactory)
        {
        }

        protected override string QueueName => Queue;
        protected override string RoutingKey => RoutingKeys.TarefaCriada;
    }
}
