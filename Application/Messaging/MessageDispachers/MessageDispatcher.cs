using Application.Interfaces.Messaging;
using Application.Messaging;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;

public class MessageDispatcher : IMessageDispatcher
{
    private readonly IServiceProvider _provider;

    public MessageDispatcher(IServiceProvider provider)
    {
        _provider = provider; 
    }

    public async Task DispatchAsync(string json)
    {
        var envelope = JsonSerializer.Deserialize<MessageEnvelope>(json);
        if (envelope == null)
            throw new Exception("Envelope inválido");

        switch (envelope.Type)
        {
            case nameof(LembreteVencimentoAtingidoEvent):
                var evento = JsonSerializer.Deserialize<LembreteVencimentoAtingidoEvent>(
                    envelope.Payload);


                var handler = _provider.GetRequiredService<IMessageHandler<LembreteVencimentoAtingidoEvent>>();

                await handler.HandleAsync(evento);
                break;

            default:
                throw new Exception($"Tipo de mensagem desconhecido: {envelope.Type}");
        }
    }
}