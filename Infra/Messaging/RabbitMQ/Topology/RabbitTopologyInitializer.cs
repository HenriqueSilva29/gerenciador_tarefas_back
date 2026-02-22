using RabbitMQ.Client;

namespace Infra.Mensageria.RabbitMQ.Topology
{
    public class RabbitTopologyInitializer : IRabbitTopologyInitializer
    {
        public async Task InitializeAsync(IChannel channel)
        {        
            await channel.ExchangeDeclareAsync(
                exchange: "app.events",
                type: ExchangeType.Topic,
                durable: true
            );
    
            await channel.QueueDeclareAsync(
                queue: "email.queue",
                durable: true,
                exclusive: false,
                autoDelete: false
            );

            await channel.QueueBindAsync(
                queue: "email.queue",
                exchange: "app.events",
                routingKey: "lembrete.email"
            );

        }
    }
}
