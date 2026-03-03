using Application.Dtos.FiltroDtos;
using Application.Utils.Paginacao;
using Domain.Entities;

namespace Application.Interfaces.UseCases.ToDoItems
{
    public interface IListarToDoItemVencidosUseCase
    {
        public Task<PaginacaoHelper<ToDoItem>> Executar(int pagina, int quantidade);
    }
}
