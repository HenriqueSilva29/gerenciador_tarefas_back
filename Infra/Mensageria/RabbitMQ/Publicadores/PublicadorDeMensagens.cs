using RabbitMQ.Client;
using System.Text.Json;
using System.Text;
using Application.Dtos.LembreteDtos;

namespace Infra.Mensageria.RabbitMQ.Publicadores
{
    public class PublicadorDeMensagens : IPublicadorDeMensagens
    {
        private readonly IRabbitChannelFactory _channelFactory;

        public PublicadorDeMensagens(IRabbitChannelFactory channelFactory)
        {
            _channelFactory = channelFactory;
        }

        public async Task PublicarAsync(LembreteMensagemDto mensagem)
        {
            await using var channel = await _channelFactory.CreateChannelAsync();

            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(mensagem));

            await channel.BasicPublishAsync(
                exchange: "",
                routingKey: "notificacoes",
                body: body
            );
        }
    }
}
