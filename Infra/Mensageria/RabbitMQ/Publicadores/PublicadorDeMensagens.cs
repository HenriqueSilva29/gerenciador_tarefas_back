using RabbitMQ.Client;
using System.Text.Json;
using System.Text;

namespace Infra.Mensageria.RabbitMQ.Publicadores
{
    public class PublicadorDeMensagens : IPublicadorDeMensagens
    {
        public async Task PublicarAsync(object mensagem)
        {
            var factory = new ConnectionFactory
            {
                Uri = new Uri("amqp://guest:guest@rabbitmq:5672/")
            };

            await using var connection = await factory.CreateConnectionAsync();
            await using var channel = await connection.CreateChannelAsync();

            await channel.QueueDeclareAsync(
                "notificacoes",
                durable: true,
                exclusive: false,
                autoDelete: false
            );

            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(mensagem));

            await channel.BasicPublishAsync(
                exchange: "",
                routingKey: "notificacoes",
                mandatory: false,
                body: body
            );
        }
    }
}
