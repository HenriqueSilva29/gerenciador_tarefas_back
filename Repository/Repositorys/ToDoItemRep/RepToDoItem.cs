using Domain.Entities.ToDoItems;
using Microsoft.EntityFrameworkCore;
using Repository.ContextEFs;
using Repository.Repositorys;
using Repository.Repositorys.IntRep;

namespace Repository.ToDoItemRep
{
    public class RepToDoItem : RepInt<ToDoItem>, IRepToDoItem
    {
        public RepToDoItem(ContextEF context) : base(context)
        {
        }
    }
}
