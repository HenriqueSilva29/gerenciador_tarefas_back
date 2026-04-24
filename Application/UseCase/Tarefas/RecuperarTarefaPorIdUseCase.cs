using Application.Dtos.Tarefas;
using Application.Interfaces.UseCases.Tarefas;
using Domain.Enums;
using Domain.Exceptions;
using Microsoft.AspNetCore.Http;
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
            var tarefa = await _rep.RecuperarPorIdAsync(id);

            if (tarefa == null)
                 throw new ExceptionApplication(EnumCodigosDeExcecao.RegistroNaoEncontrado, $"Tarefa não encontrada no banco de dados. Id: {id}", StatusCodes.Status409Conflict);

            return new TarefaResponse
            {
                Id = tarefa.Id
            };

        }
    }
}
