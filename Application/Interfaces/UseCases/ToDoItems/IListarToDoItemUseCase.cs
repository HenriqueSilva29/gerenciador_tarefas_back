using Application.Dtos.FiltroDtos;
using Application.Utils.Paginacao;
using Domain.Entities;

namespace Application.Interfaces.UseCases.ToDoItems
{
    public interface IListarToDoItemUseCase
    {
        public Task<PaginacaoHelper<ToDoItem>> Executar(FiltroToDoItemDto parametros);
    }
}