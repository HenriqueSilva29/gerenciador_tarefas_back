using Domain.Comum;
using Microsoft.EntityFrameworkCore;
using Repository.ContextosEF;

namespace Repository.Repositorios
{

    public class Repositorio<T, TId> : IRepositorio<T, TId>
    where T : class, IEntidadeId<TId>
    {
        public readonly ContextEF _context;
        private readonly DbSet<T> _dbSet;

        public Repositorio(ContextEF context)
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

        void IRepositorio<T, TId>.Atualizar(T entity)
        {
             _dbSet.Update(entity);
        }

        void IRepositorio<T, TId>.Remover(T entity)
        {
             _dbSet.Remove(entity);
        }

        public async Task<T?> RecuperarPorIdAsync(TId id)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.Id!.Equals(id));
        }

    }
}

