using Application.Dtos.Filtros.Tarefas;
using Application.Utils.Paginacao;
using Domain.Entities;

namespace Application.Interfaces.UseCases.Tarefas
{
    public interface IListarTarefasUseCase
    {
        public Task<PaginacaoHelper<Tarefa>> Executar(TarefaFiltroRequest parametros);
    }
}