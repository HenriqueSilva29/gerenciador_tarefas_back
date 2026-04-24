using RabbitMQ.Client.Events;

namespace Infra.Messaging.RabbitMQ.Consumidores
{
    public static class HelperConsumer
    {
        public static int GetRetryCount(BasicDeliverEventArgs args)
        {
            if (args.BasicProperties?.Headers == null) return 0;

            if (!args.BasicProperties.Headers.TryGetValue("x-death", out var deathHeader))
                return 0;

            try
            {
                var deaths = deathHeader as IList<object>;
                if (deaths == null || deaths.Count == 0)
                    return 0;

                var death = deaths[0] as IDictionary<string, object>;
                if (death == null)
                    return 0;

                if (!death.TryGetValue("count", out var countObj))
                    return 0;

                return Convert.ToInt32(countObj);
            }
            catch
            {
                return 0;
            }
        }
    }
}
