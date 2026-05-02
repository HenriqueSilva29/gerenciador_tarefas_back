using Infra.Messaging.RabbitMQ.Channels;
using Infra.Messaging.RabbitMQ;
using Infra.Messaging.RabbitMQ.Consumidores;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Infra.Messaging.RabbitMQ.Consumidores.Notificacoes
{
    public class NotificacaoCriadaConsumer : RabbitMessageConsumerBase
    {
        public const string Queue = "notificacao.criada.tempo-real.queue";

        public NotificacaoCriadaConsumer(
            IRabbitChannelFactory channelFactory,
            IServiceScopeFactory scopeFactory,
            ILogger<RabbitMessageConsumerBase> logger)
            : base(channelFactory, scopeFactory, logger)
        {
        }

        protected override string QueueName => Queue;
        protected override string RoutingKey => RoutingKeys.NotificacaoCriada;
    }
}
