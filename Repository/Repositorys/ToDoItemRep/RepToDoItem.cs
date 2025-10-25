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

        public async Task<IEnumerable<ToDoItem>> RecuperarTodos()
        {
            return await RecuperarTodos();
        }

        public async Task<ToDoItem> RecuperarPorId(int id)
        {
            return await RecuperarPorId(id);
        }

        public async Task Adicionar(ToDoItem entity)
        {
            await Adicionar(entity);
        }

        public async Task Atualizar(ToDoItem entity)
        {
            await Atualizar(entity);
        }

        public async Task Remover(ToDoItem entity)
        {               
            await Remover(entity);
        }

        public async Task<IEnumerable<ToDoItem>> Filtrar(Expression<Func<ToDoItem, bool>> parametros)
        {
            return await Filtrar(parametros);
        }
    }

}
