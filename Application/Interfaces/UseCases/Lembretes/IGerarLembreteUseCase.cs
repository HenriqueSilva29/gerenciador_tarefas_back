using Application.Events.Tarefas;

namespace Application.Interfaces.UseCases.Lembretes
{
    public interface IGerarLembreteUseCase
    {
        public Task ExecuteAsync(TarefaCriadaEvent idtarefa);
    }
}
