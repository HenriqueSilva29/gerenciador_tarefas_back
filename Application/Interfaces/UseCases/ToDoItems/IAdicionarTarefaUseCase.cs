using Application.Dtos.TarefaDtos;

namespace Application.Interfaces.UseCases.Tarefas
{
    public interface IAdicionarTarefaUseCase
    {
        public Task Executar(AdicionarTarefaDto dto);
    }
}
