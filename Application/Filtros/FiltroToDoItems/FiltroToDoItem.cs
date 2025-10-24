using Application.Dtos.Filtros;
using Domain.ToDoItem;
using Repository.Repositorys;

namespace Application.Filtros.FiltroToDoItems
{
    public class FiltroToDoItem
    {
        private readonly IQueryable<ToDoItem> _toDoItem;
        private readonly IRepository<ToDoItem> _rep;

        public FiltroToDoItem(IQueryable<ToDoItem> toDoItem, IRepository<ToDoItem> rep)
        {
            _toDoItem = toDoItem;
            _rep = rep;
        }

        /* Método que aplica todos os filtros acumulados
        public Task<IQueryable<ToDoItem>> BuscarToDoItems(FiltroToDoItemDto filtro)
        {

        }*/
    }
}