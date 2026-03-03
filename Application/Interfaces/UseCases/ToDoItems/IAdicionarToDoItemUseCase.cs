using Application.Dtos.ToDoItemDtos;

namespace Application.Interfaces.UseCases.ToDoItems
{
    public interface IAdicionarToDoItemUseCase
    {
        public Task Executar(AdicionarToDoItemDto dto);
    }
}
