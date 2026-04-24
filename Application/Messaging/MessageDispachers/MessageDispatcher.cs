using Application.Events.Tarefas;
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
        var Types = new Dictionary<string, Type>()
        {
            {nameof(TarefaCriadaEvent), typeof(TarefaCriadaEvent)},

        };

        var envelope = JsonSerializer.Deserialize<MessageEnvelope>(json);

        if (envelope == null)
            throw new Exception("Envelope inválido");

        if(!Types.TryGetValue(envelope.Type, out var eventType))
        {
            throw new ApplicationException($"Tipo não mapeado: {envelope.Type} ");
        }

        var evento = JsonSerializer.Deserialize(envelope.Payload, eventType);

        var handlerType = typeof(IMessageHandler<>).MakeGenericType(eventType);

        var handler = _provider.GetRequiredService(handlerType);

        var method = handlerType.GetMethod("HandleAsync");

        await (Task)method.Invoke(handler, new[] { evento });
    }
}