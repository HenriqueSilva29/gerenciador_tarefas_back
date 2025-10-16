using Domain.ToDoItem;
using Repository.ContextEFs;
using Microsoft.EntityFrameworkCore;

namespace Repository.ToDoItemRep
{
    public class RepToDoItem : IRepToDoItem
    {
        private readonly ContextEF _context;

        public RepToDoItem(ContextEF context)
        {
            _context = context;
        }
        public async Task<IEnumerable<ToDoItem>> RecuperarTodos()
        {
            return await _context.ToDoItems.ToListAsync();
        }

        public async Task<ToDoItem> RecuperarPorId(int id)
        {
            return await _context.ToDoItems.FindAsync(id);
        }

        public async Task Adicionar(ToDoItem ToDoItem)
        {
            await _context.ToDoItems.AddAsync(ToDoItem);
            await _context.SaveChangesAsync();
        }

        public async Task Atualizar(ToDoItem ToDoItem)
        {
            _context.ToDoItems.Update(ToDoItem);
            await _context.SaveChangesAsync();
        }

        public async Task Remover(ToDoItem toDoItem)
        {
                _context.ToDoItems.Remove(toDoItem);
                await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ToDoItem>> Filtrar(string category, bool? isCompleted)
        {
            var query = _context.ToDoItems.AsQueryable();

            if (!string.IsNullOrEmpty(category))
            {
                query = query.Where(t => t.Categoria == category);
            }

            if (isCompleted.HasValue)
            {
                query = query.Where(t => t.Concluido == isCompleted);
            }

            return await query.ToListAsync();
        }
    }

}
