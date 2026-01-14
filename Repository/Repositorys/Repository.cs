using Microsoft.EntityFrameworkCore;
using Repository.ContextEFs;

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

        public IEnumerable<T> AsEnumerable()
        {
            return _dbSet.AsEnumerable();
        }


        public async Task Adicionar(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task Atualizar(T entity)
        {
             _dbSet.Update(entity);
        }

        public async Task Remover(T entity)
        {
             _dbSet.Remove(entity);
        }

    }
}
