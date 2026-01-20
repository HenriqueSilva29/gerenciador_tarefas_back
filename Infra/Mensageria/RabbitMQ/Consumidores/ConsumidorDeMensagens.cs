using Application.Dtos.LembreteDtos;
using Application.Services.ServLembretes;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace Infra.Mensageria.RabbitMQ.Consumidores
{
    public class ConsumidorDeMensagens
    {
        private readonly IChannel _channel;
        private readonly IServiceScopeFactory _scopeFactory;
        private const string QueueName = "notificacoes";

        public ConsumidorDeMensagens(IChannel channel, IServiceScopeFactory scopeFactory)
        {
            _channel = channel;
            _scopeFactory = scopeFactory;
        }

        public async Task IniciarAsync()
        {
            await _channel.QueueDeclareAsync(
                queue: QueueName,
                durable: true,
                exclusive: false,
                autoDelete: false
            );

            Console.WriteLine("Iniciando consumidor");

            var consumer = new AsyncEventingBasicConsumer(_channel);
            consumer.ReceivedAsync += async (_, args) =>
            {
                using var scope = _scopeFactory.CreateScope();
                var service = scope.ServiceProvider.GetRequiredService<IServLembrete>();

                var mensagemJson = Encoding.UTF8.GetString(args.Body.ToArray());

                var dto = JsonSerializer.Deserialize<LembreteMensagemDto>(mensagemJson);

                if (dto != null)
                {

                    try
                    {
                        await service.AgendarLembrete(dto.IdLembrete);

                        await _channel.BasicAckAsync(args.DeliveryTag, false);
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine($"ERRO: " + e.StackTrace);
                        await _channel.BasicNackAsync(args.DeliveryTag, false, requeue: true);
                    }
                }
                else
                {
                    Console.WriteLine("Erro ao desserializar a mensagem.");
                    await _channel.BasicNackAsync(args.DeliveryTag, false, requeue: true);
                }
            };

            await _channel.BasicQosAsync(
                prefetchSize: 0,
                prefetchCount: 1,
                global: false
            );

            await _channel.BasicConsumeAsync(
                queue: QueueName,
                autoAck: false,
                consumer: consumer
            );

            Console.WriteLine("Consumer registrado com sucesso .");
        }

    }
}
