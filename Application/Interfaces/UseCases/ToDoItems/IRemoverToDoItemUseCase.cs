namespace Application.Interfaces.UseCases.ToDoItems
{
    public interface IRemoverToDoItemUseCase
    {
        public Task Executar(int id);
    }
}
