using Application.Dtos.FiltroDtos;
using Application.Dtos.ToDoItemDtos;
using Application.Utils.Paginacao;
using Application.Views;
using Domain.ToDoItems;

namespace Application.Services.ServToDoItems
{
    public interface IServToDoItem
    {
        Task<PaginacaoHelper<ToDoItemView>> RecuperarTodos(int pagina, int quantidade);
        Task Adicionar(AdicionarToDoItemDto dto);
        Task Atualizar(int id, AtualizarToDoItemDto dto);
        Task Remover(int id);
        Task<PaginacaoHelper<ToDoItem>> ListarFiltradoAsync(FiltroToDoItemDto parametros);
        Task<PaginacaoHelper<ToDoItem>> RecuperarTarefasVencidas(int pagina, int quantidade);
        Task AtualizarPrioridade(int id, AtualizarPrioridadeDto dto);
    }
}
