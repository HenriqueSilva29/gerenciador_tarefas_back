using Application.Dtos.FiltroDtos;
using Application.Dtos.ToDoItemDtos;
using Application.Views;
using Domain.ToDoItems;

namespace Application.Services.ServToDoItems
{
    public interface IServToDoItem
    {
        Task<List<ToDoItemView>> RecuperarTodos();
        Task Adicionar(AdicionarToDoItemDto dto);
        Task Atualizar(int id, AtualizarToDoItemDto dto);
        Task Remover(int id);
        Task<List<ToDoItem>> ListarFiltradoAsync(FiltroToDoItemDto parametros);
        Task<List<ToDoItem>> RecuperarTarefasVencidas();
        Task AtualizarPrioridade(int id, AtualizarPrioridadeDto dto);
    }
}
