namespace Infra.Messaging.RabbitMQ.Topology
{
    public static class RabbitTopologyNames
    {
        public const string EventsExchange = "app.events";
        public const string RetryExchange = "app.events.retry";
        public const string DlqExchange = "app.events.dlq";
    }
}
