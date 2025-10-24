using Microsoft.EntityFrameworkCore;
using Repository.ContextEFs;
using System.Linq.Expressions;

namespace Repository.Repositorys
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ContextEF _context;
        private readonly DbSet<T> _dbSet;

        public Repository(ContextEF context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public IQueryable<T> AsQueryable()
        {
            return _dbSet.AsQueryable();
        }

        public async Task<IEnumerable<T>> RecuperarTodos()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> RecuperarPorId(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task Adicionar(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Atualizar(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Remover(T entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> Filtrar(Expression<Func<T, bool>> filtro)
        {
            return await _dbSet.Where(filtro).ToListAsync();
        }
    }
}
