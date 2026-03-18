using Application.Dtos.Tarefas;
using Application.Interfaces.UseCases.Tarefas;
using Repository.TarefaRep;

namespace Application.UseCase.ToDoItems
{
    public class RecuperarTarefaPorIdUseCase : IRecuperarTarefaPorIdUseCase
    {
        public readonly IRepTarefa _rep;
        public RecuperarTarefaPorIdUseCase
        (
            IRepTarefa rep
        )
        {
            _rep = rep;
        }

        public async Task<TarefaResponse> Executar(int id)
        {
            var tarefa = await _rep.RecuperarPorId(id);

            if (tarefa == null)
                return null;

            return new TarefaResponse
            {
                Id = tarefa.Id
            };

        }
    }
}
