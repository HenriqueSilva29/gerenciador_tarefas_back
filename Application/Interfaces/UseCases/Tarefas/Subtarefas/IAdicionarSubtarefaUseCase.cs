using Application.Dtos.Tarefas.Subtarefas;

namespace Application.Interfaces.UseCases.Tarefas.Subtarefas
{
    public interface IAdicionarSubtarefaUseCase
    {
        public Task<SubtarefaResponse> Executar(AdicionarSubtarefaRequest dto);
    }
}
