using Infra.Messaging.RabbitMQ.Channels;
using Infra.Messaging.RabbitMQ;
using Infra.Messaging.RabbitMQ.Consumidores;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Infra.Messaging.RabbitMQ.Consumidores.Tarefas
{
    public class GerarLembreteConsumer : RabbitMessageConsumerBase
    {
        public const string Queue = "tarefa.criada.gerar-lembrete.queue";

        public GerarLembreteConsumer(
            IRabbitChannelFactory channelFactory,
            IServiceScopeFactory scopeFactory,
            ILogger<RabbitMessageConsumerBase> logger)
            : base(channelFactory, scopeFactory, logger)
        {
        }

        protected override string QueueName => Queue;
        protected override string RoutingKey => RoutingKeys.TarefaCriada;
    }
}
