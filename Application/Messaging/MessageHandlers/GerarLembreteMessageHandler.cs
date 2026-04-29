using Application.Funcionalidades.Tarefas.Eventos;
using Application.Interfaces.Messaging;
using Application.Funcionalidades.Lembretes.Contratos.CasosDeUso;

namespace Application.Messaging.MessageHandlers
{
    public class GerarLembreteMessageHandler : IMessageHandler<TarefaCriadaEvento>
    {
        readonly IGerarLembreteCasoDeUso _useCase;

        public GerarLembreteMessageHandler(IGerarLembreteCasoDeUso useCase)
        {
            _useCase = useCase;
        }

        public Task HandleAsync(TarefaCriadaEvento evento)
            => _useCase.ExecuteAsync(evento);
  
    }
}
