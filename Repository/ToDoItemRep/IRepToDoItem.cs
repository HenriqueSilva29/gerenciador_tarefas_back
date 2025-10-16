using Domain.ToDoItem;

namespace Repository.ToDoItemRep
{
    public interface IRepToDoItem
    {
        Task<IEnumerable<ToDoItem>> RecuperarTodos();
        Task<ToDoItem> RecuperarPorId(int id);
        Task Adicionar(ToDoItem toDoItem);
        Task Atualizar(ToDoItem toDoItem);
        Task Remover(ToDoItem todoItem);
        Task<IEnumerable<ToDoItem>> Filtrar(string category, bool? isCompleted);
    }
}
