using Application.Funcionalidades.Notificacoes.Eventos;
using Application.Funcionalidades.Tarefas.Eventos;
using Application.Interfaces.Messaging;
using Application.Messaging;
using Application.Observabilidade;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Text.Json;

public class MessageDispatcher : IMessageDispatcher
{
    private readonly IServiceProvider _provider;
    private readonly ILogger<MessageDispatcher> _logger;

    public MessageDispatcher(
        IServiceProvider provider,
        ILogger<MessageDispatcher> logger)
    {
        _provider = provider;
        _logger = logger;
    }

    public async Task DispatchAsync(string json)
    {
        var types = new Dictionary<string, Type>()
        {
            { nameof(TarefaCriadaEvento), typeof(TarefaCriadaEvento) },
            { nameof(NotificacaoCriadaEvento), typeof(NotificacaoCriadaEvento) },
        };

        var envelope = JsonSerializer.Deserialize<MessageEnvelope>(json);

        if (envelope == null)
            throw new Exception("Envelope invalido");

        if (!types.TryGetValue(envelope.Type, out var eventType))
        {
            throw new ApplicationException($"Tipo nao mapeado: {envelope.Type} ");
        }

        var evento = JsonSerializer.Deserialize(envelope.Payload, eventType);

        var handlerType = typeof(IMessageHandler<>).MakeGenericType(eventType);
        var handler = _provider.GetRequiredService(handlerType);
        var method = handlerType.GetMethod("HandleAsync");

        using var activity = ObservabilidadeFonte.ActivitySource.StartActivity(
            $"message handler {eventType.Name}",
            ActivityKind.Internal);

        activity?.SetTag("messaging.message.type", eventType.Name);
        activity?.SetTag("correlation.id", envelope.CorrelationId);

        _logger.LogInformation(
            "Despachando evento {Evento} para handler {Handler}",
            eventType.Name,
            handlerType.Name);

        await (Task)method!.Invoke(handler, new[] { evento })!;
    }
}
