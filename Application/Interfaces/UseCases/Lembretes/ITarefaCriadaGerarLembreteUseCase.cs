using Application.Events.Tarefas;
using Domain.Entities;

namespace Application.Interfaces.UseCases.Lembretes
{
    public interface ITarefaCriadaGerarLembreteUseCase
    {
        public Task ExecuteAsync(TarefaCriadaEvent idtarefa);
    }
}
