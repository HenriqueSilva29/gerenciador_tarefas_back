using Application.Dtos.LembreteDtos;
using Application.Interfaces.Messaging;
using Domain.Entities.Lembretes;
using Infra.Mensageria.RabbitMQ.Channels;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace Infra.Mensageria.RabbitMQ.Publicadores
{
    public class RabbitNotificacaoPublisher : INotificacaoPublisher
    {
        private readonly IRabbitChannelFactory _channelFactory;

        public RabbitNotificacaoPublisher(IRabbitChannelFactory channelFactory)
        {
            _channelFactory = channelFactory;
        }

        public async Task PublicarAsync(Lembrete lembrete)
        {
            var dto = new LembreteMensagemDto
            {
                IdLembrete = lembrete.Id,
                Texto = lembrete.Texto,
                IdTarefa = lembrete.CodigoToDoItem
            };

            using var channel = await _channelFactory.CreateChannelAsync();

            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(dto));

            await channel.BasicPublishAsync(
                exchange: "",
                routingKey: "notificacoes",
                body: body
            );
        }
    }
}
