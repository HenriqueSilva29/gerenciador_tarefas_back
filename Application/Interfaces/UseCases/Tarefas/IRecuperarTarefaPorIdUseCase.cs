using Application.Dtos.Tarefas;

namespace Application.Interfaces.UseCases.Tarefas
{
    public interface IRecuperarTarefaPorIdUseCase
    {
        Task<TarefaResponse> Executar(int id);
    }
}
