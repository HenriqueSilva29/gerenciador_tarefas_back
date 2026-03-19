using Infra.Mensageria.RabbitMQ.Topology;
using RabbitMQ.Client;

namespace Infra.Messaging.RabbitMQ.Topology.Topologies.Lembretes
{
    public class NotificacaoEmailLembreteVencimentoTopology : IRabbitTopology
    {
        public async Task ConfigureAsync(IChannel channel)
        {
            var exchange = "app.events";
            var exchangeDlq = "app.events.dlq";
            var exchangeRetry = "app.events.retry";

            var queue = "notification.email.lembrete-vencimento.queue";
            var dlq = $"{queue}.dlq";
            var retry = $"{queue}.retry";

            // Exchanges
            await channel.ExchangeDeclareAsync(exchange, ExchangeType.Direct, true);
            await channel.ExchangeDeclareAsync(exchangeDlq, ExchangeType.Direct, true);
            await channel.ExchangeDeclareAsync(exchangeRetry, ExchangeType.Direct, true);

            // DLQ
            await channel.QueueDeclareAsync(dlq, true, false, false);
            await channel.QueueBindAsync(dlq, exchangeDlq, RoutingKeys.LembreteVencimentoAtingidoV1);

            // Retry
            var retryArgs = new Dictionary<string, object>
            {
                { "x-message-ttl", 5000 },
                { "x-dead-letter-exchange", exchange },
                { "x-dead-letter-routing-key", RoutingKeys.LembreteVencimentoAtingidoV1 }
            };

            await channel.QueueDeclareAsync(retry, true, false, false, retryArgs);
            await channel.QueueBindAsync(retry, exchangeRetry, RoutingKeys.LembreteVencimentoAtingidoV1);

            // Fila Principal
            var mainArgs = new Dictionary<string, object>()
            {
                { "x-dead-letter-exchange", exchangeRetry },
                { "x-dead-letter-routing-key", RoutingKeys.LembreteVencimentoAtingidoV1 }
            };

            await channel.QueueDeclareAsync(queue, true, false, false, mainArgs);
            await channel.QueueBindAsync(queue, exchange, RoutingKeys.LembreteVencimentoAtingidoV1);
        }
    }
}
