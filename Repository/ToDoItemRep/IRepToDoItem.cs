using Domain.ToDoItem;

namespace Repository.ToDoItemRep
{
    public interface IRepToDoItem
    {
        Task<IEnumerable<ToDoItem>> RecuperarTodos();
        Task<ToDoItem> RecuperarPorId(int id);
        Task Adicionar(ToDoItem todoItem);
        Task Atualizar(ToDoItem todoItem);
        Task Remover(int id);
        Task<IEnumerable<ToDoItem>> Filtrar(string category, bool? isCompleted);
    }
}
