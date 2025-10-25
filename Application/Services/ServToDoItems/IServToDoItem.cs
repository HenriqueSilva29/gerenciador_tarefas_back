using Application.Dtos.Filtros.FiltroToDoItemDtos;
using Application.Dtos.ToDoItemDtos;
using Domain.ToDoItems;

namespace Application.Services.ServToDoItems
{
    public interface IServToDoItem
    {
        Task<IEnumerable<ToDoItem>> RecuperarTodos();
        Task Adicionar(ToDoItemDto todoItemDto);
        Task Atualizar(int id, ToDoItemDto toDoItemDto);
        Task Remover(int id);
        Task<IEnumerable<ToDoItem>> Filtrar(FiltroToDoItemDto parametros);
    }
}
