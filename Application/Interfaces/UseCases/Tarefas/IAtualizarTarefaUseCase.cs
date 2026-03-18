using Application.Dtos.Tarefas;

namespace Application.Interfaces.UseCases.Tarefas
{
    public interface IAtualizarTarefaUseCase
    {
        public Task Executar(int id, UpdateTarefaRequest dto);
    }
}
