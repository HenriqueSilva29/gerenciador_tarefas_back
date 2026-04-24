using Application.Events.Tarefas;
using Domain.Entities;

namespace Application.Interfaces.UseCases.Lembretes
{
    public interface IGerarLembreteUseCase
    {
        public Task ExecuteAsync(TarefaCriadaEvent idtarefa);
    }
}
