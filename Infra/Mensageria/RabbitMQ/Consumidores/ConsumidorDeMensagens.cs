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
        private const string LogFile = "rabbit-log.txt";

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

            using var scope = _scopeFactory.CreateScope();
            var service = scope.ServiceProvider.GetRequiredService<IServLembrete>();

            var consumer = new AsyncEventingBasicConsumer(_channel);
            consumer.ReceivedAsync += async (_, args) =>
            {
                var mensagemJson = Encoding.UTF8.GetString(args.Body.ToArray());

                // Desserializa para LembreteMensagemDto
                var dto = JsonSerializer.Deserialize<LembreteMensagemDto>(mensagemJson);

                if (dto != null)
                {
                    Console.WriteLine($"📥 Mensagem recebida: IdLembrete={dto.IdLembrete}, IdTarefa={dto.IdTarefa}, Texto={dto.Texto}");
                    await RegistrarEmArquivoAsync($"[RECEBIDA] IdLembrete={dto.IdLembrete}, IdTarefa={dto.IdTarefa}, Texto={dto.Texto}");

                    try
                    {
                        // Simula processamento
                        await Task.Delay(10000);

                        await service.MarcarLembreteComoEnviado(dto.IdLembrete);

                        await RegistrarEmArquivoAsync($"[CONSUMIDA] IdLembrete={dto.IdLembrete}, IdTarefa={dto.IdTarefa}, Texto={dto.Texto}");
                        Console.WriteLine("✅ Mensagem consumida");

                        await _channel.BasicAckAsync(args.DeliveryTag, false);
                    }
                    catch
                    {
                        await _channel.BasicNackAsync(args.DeliveryTag, false, requeue: true);
                    }
                }
                else
                {
                    Console.WriteLine("❌ Erro ao desserializar a mensagem.");
                    await _channel.BasicNackAsync(args.DeliveryTag, false, requeue: true);
                }
            };

            await _channel.BasicConsumeAsync(
                queue: QueueName,
                autoAck: false,
                consumer: consumer
            );


            Console.WriteLine("🎧 Consumer RabbitMQ iniciado.");
        }


        private static async Task RegistrarEmArquivoAsync(string texto)
        {
            var linha = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {texto}";
            await File.AppendAllTextAsync(LogFile, linha + Environment.NewLine);
        }
    }
}
