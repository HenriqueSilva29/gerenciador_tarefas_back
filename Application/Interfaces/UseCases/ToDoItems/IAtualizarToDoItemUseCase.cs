using Application.Dtos.ToDoItemDtos;

namespace Application.Interfaces.UseCases.ToDoItems
{
    public interface IAtualizarToDoItemUseCase
    {
        public Task Executar(int id, AtualizarToDoItemDto dto);
    }
}
