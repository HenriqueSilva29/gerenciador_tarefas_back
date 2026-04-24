using Application.Events.Tarefas;
using Application.Interfaces.Messaging;
using Application.Interfaces.UseCases.Lembretes;

namespace Application.Messaging.MessageHandlers
{
    public class GerarLembreteMessageHandler : IMessageHandler<TarefaCriadaEvent>
    {
        readonly IGerarLembreteUseCase _useCase;

        public GerarLembreteMessageHandler(IGerarLembreteUseCase useCase)
        {
            _useCase = useCase;
        }
        public async Task HandleAsync(TarefaCriadaEvent evento)
        {
            await _useCase.ExecuteAsync(evento);
        }
    }
}
