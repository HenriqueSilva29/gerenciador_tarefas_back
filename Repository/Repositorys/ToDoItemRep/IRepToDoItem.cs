using Domain.Entities;
using Repository.Repositorys;

namespace Repository.ToDoItemRep
{
    public interface IRepToDoItem : IRepository<ToDoItem, int>
    {
        // Métodos reutilizáveis devem serem inseridos no rep genérico
    }
}
