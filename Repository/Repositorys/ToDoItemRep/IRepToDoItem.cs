using Domain.ToDoItem;
using Repository.Repositorys;
using System.Linq.Expressions;

namespace Repository.ToDoItemRep
{
    public interface IRepToDoItem : IRepository<ToDoItem>
    {
        // Métodos reutilizáveis devem serem inseridos no rep genérico
    }
}
