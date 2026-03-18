using Application.Dtos.FiltroDtos;
using Application.Utils.Paginacao;
using Domain.Entities;

namespace Application.Interfaces.UseCases.Tarefas
{
    public interface IListarTarefaUseCase
    {
        public Task<PaginacaoHelper<Tarefa>> Executar(TarefaFiltroRequest parametros);
    }
}