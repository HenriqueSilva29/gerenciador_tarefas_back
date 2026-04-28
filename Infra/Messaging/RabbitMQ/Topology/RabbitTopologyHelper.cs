using RabbitMQ.Client;

namespace Infra.Messaging.RabbitMQ.Topology
{
    public static class RabbitTopologyHelper
    {
        public static async Task DeclareConsumerTopologyAsync(
            IChannel channel,
            string queue,
            string routingKey,
            int retryDelayInMilliseconds = 5000)
        {
            var dlq = $"{queue}.dlq";
            var retry = $"{queue}.retry";

            await channel.ExchangeDeclareAsync(RabbitTopologyNames.EventsExchange, ExchangeType.Direct, true);
            await channel.ExchangeDeclareAsync(RabbitTopologyNames.DlqExchange, ExchangeType.Direct, true);
            await channel.ExchangeDeclareAsync(RabbitTopologyNames.RetryExchange, ExchangeType.Direct, true);

            await channel.QueueDeclareAsync(dlq, true, false, false);
            await channel.QueueBindAsync(dlq, RabbitTopologyNames.DlqExchange, routingKey);

            var retryArgs = new Dictionary<string, object>
            {
                { "x-message-ttl", retryDelayInMilliseconds },
                { "x-dead-letter-exchange", RabbitTopologyNames.EventsExchange },
                { "x-dead-letter-routing-key", routingKey }
            };

            await channel.QueueDeclareAsync(retry, true, false, false, retryArgs);
            await channel.QueueBindAsync(retry, RabbitTopologyNames.RetryExchange, routingKey);

            var mainArgs = new Dictionary<string, object>
            {
                { "x-dead-letter-exchange", RabbitTopologyNames.RetryExchange },
                { "x-dead-letter-routing-key", routingKey }
            };

            await channel.QueueDeclareAsync(queue, true, false, false, mainArgs);
            await channel.QueueBindAsync(queue, RabbitTopologyNames.EventsExchange, routingKey);
        }
    }
}
