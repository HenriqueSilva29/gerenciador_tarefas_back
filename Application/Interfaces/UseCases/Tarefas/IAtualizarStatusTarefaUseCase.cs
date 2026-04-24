using Application.Dtos.Tarefas;

namespace Application.Interfaces.UseCases.Tarefas
{
    public interface IAtualizarStatusTarefaUseCase
    {
        public Task Executar(int id, UpdateStatusTarefaRequest dto);
    }
}
