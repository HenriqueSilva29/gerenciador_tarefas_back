using Application.Dtos.LembreteDtos;
using Application.Interfaces.Messaging;
using Application.Messaging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Text.Json;

public class MessageDispatcher : IMessageDispatcher
{
    private readonly IServiceProvider _provider;
    readonly ILogger<MessageDispatcher> _logger;

    public MessageDispatcher(IServiceProvider provider, ILogger<MessageDispatcher> logger)
    {
        _provider = provider;
        _logger = logger;   
    }

    public async Task DispatchAsync(string json)
    {
        var envelope = JsonSerializer.Deserialize<MessageEnvelope>(json);
        if (envelope == null)
            throw new Exception("Envelope inválido");

        _logger.LogError("Envelope Type: " + envelope.Type);

        switch (envelope.Type)
        {
            case "lembrete.email":
                var dto = JsonSerializer.Deserialize<LembreteMensagemDto>(envelope.Payload);
                var handler = _provider.GetRequiredService<IMessageHandler<LembreteMensagemDto>>();
                await handler.HandleAsync(dto, CancellationToken.None);
                break;

            default:
                throw new Exception($"Tipo de mensagem desconhecido: {envelope.Type}");
        }
    }
}