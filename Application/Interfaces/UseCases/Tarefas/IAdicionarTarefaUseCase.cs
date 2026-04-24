using Application.Dtos.Tarefas;

namespace Application.Interfaces.UseCases.Tarefas
{
    public interface IAdicionarTarefaUseCase
    {
        public Task<TarefaResponse> Executar(CreateTarefaRequest dto);
    }
}
