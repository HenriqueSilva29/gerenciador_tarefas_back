using Application.Dtos.Tarefas;

namespace Application.Interfaces.UseCases.Tarefas
{
    public interface IRecuperarHistoricoTarefaUseCase
    {
        Task<HistoricoTarefaResponse> Executar(int id);
    }
}
