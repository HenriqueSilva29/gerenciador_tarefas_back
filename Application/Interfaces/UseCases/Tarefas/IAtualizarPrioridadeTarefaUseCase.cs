using Application.Dtos.Tarefas;
using Application.Dtos.Tarefas;

namespace Application.Interfaces.UseCases.Tarefas
{
    public interface IAtualizarPrioridadeTarefaUseCase
    {
        public Task Executar(int id, UpdatePrioridadeTarefaRequest dto);
    }
}
