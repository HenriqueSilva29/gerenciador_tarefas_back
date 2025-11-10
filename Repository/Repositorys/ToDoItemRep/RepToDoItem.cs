using Domain.ToDoItems;
using Repository.ContextEFs;
using Repository.Repositorys;
using System.Linq.Expressions;

namespace Repository.ToDoItemRep
{
    
    public class RepToDoItem : Repository<ToDoItem>, IRepToDoItem
    {
        public RepToDoItem(ContextEF context) : base(context)
        {
        }
     
    }

}
