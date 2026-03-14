using Application.Dtos.TarefaDtos;

namespace Application.Interfaces.UseCases.Tarefas
{
    public interface IAtualizarTarefaUseCase
    {
        public Task Executar(int id, AtualizarTarefaDto dto);
    }
}
