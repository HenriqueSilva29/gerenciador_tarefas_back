using Application.Dtos.ToDoItemDtos;

namespace Application.Interfaces.UseCases.ToDoItems
{
    public interface IAtualizarPrioridadeToDoItemUseCase
    {
        public Task Executar(int id, AtualizarPrioridadeToDoItemDto dto);
    }
}
