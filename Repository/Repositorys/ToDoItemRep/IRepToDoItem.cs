using Domain.ToDoItems;
using Repository.Repositorys;
using Repository.Repositorys.IntRep;
using System.Linq.Expressions;

namespace Repository.ToDoItemRep
{
    public interface IRepToDoItem : IRepInt<ToDoItem>
    {
        // Métodos reutilizáveis devem serem inseridos no rep genérico
    }
}
