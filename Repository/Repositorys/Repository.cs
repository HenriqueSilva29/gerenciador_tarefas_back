using Domain.Common;
using Microsoft.EntityFrameworkCore;
using Repository.ContextEFs;

namespace Repository.Repositorys
{

    public class Repository<T, TId> : IRepository<T, TId>
    where T : class, IEntityId<TId>
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


        public void Adicionar(T entity)
        {
             _dbSet.AddAsync(entity);
        }

        void IRepository<T, TId>.Atualizar(T entity)
        {
             _dbSet.Update(entity);
        }

        void IRepository<T, TId>.Remover(T entity)
        {
             _dbSet.Remove(entity);
        }

        public async Task<T?> RecuperarPorId(TId id)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.Id!.Equals(id));
        }

    }
}
