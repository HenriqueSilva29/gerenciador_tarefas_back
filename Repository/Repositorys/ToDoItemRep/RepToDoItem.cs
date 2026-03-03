using Domain.Entities;
using Repository.ContextEFs;
using Repository.Repositorys;

namespace Repository.ToDoItemRep
{
    public class RepToDoItem : Repository<ToDoItem, int>, IRepToDoItem
    {
        public RepToDoItem(ContextEF context) : base(context)
        {
        }
    }
}
