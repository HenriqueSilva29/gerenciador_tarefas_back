using Application.Events.Tarefas;
using Application.Interfaces.Messaging;
using Application.Interfaces.UseCases.Lembretes;

namespace Application.Messaging.MessageHandlers
{
    public class TarefaCriadaGerarLembreteMessageHandler : IMessageHandler<TarefaCriadaEvent>
    {
        readonly ITarefaCriadaGerarLembreteUseCase _useCase;

        public TarefaCriadaGerarLembreteMessageHandler(ITarefaCriadaGerarLembreteUseCase useCase)
        {
            _useCase = useCase;
        }
        public async Task HandleAsync(TarefaCriadaEvent evento)
        {
            await _useCase.ExecuteAsync(evento);
        }
    }
}
