using Application.Dtos.FiltroDtos;
using Application.Dtos.ToDoItemDtos;
using Application.Utils.Paginacao;
using Application.Views;
using Domain.Entities;

namespace Application.Services.ServToDoItems
{
    public interface IServToDoItem
    {
        Task AdicionarTarefa(AdicionarToDoItemDto dto);
        Task AtualizarTarefa(int id, AtualizarToDoItemDto dto);
        Task RemoverTarefa(int id);
        Task<PaginacaoHelper<ToDoItem>> ListarTarefas(FiltroToDoItemDto parametros);
        Task<PaginacaoHelper<ToDoItem>> RecuperarTarefasVencidas(int pagina, int quantidade);
        Task AtualizarPrioridade(int id, AtualizarPrioridadeToDoItemDto dto);
    }
}
