namespace Infra.Messaging.RabbitMQ
{
    public static class RabbitTopologyNames
    {
        public const string EventsExchange = "app.events";
        public const string RetryExchange = "app.events.retry";
        public const string DlqExchange = "app.events.dlq";
    }
}
