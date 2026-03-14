using Application.Dtos.TarefaDtos;

namespace Application.Interfaces.UseCases.Tarefas
{
    public interface IAtualizarPrioridadeTarefaUseCase
    {
        public Task Executar(int id, AtualizarPrioridadeTarefaDto dto);
    }
}
