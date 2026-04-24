using Domain.Common;

namespace Repository.Repositorys
{
    public interface IRepository<T, TId> where T : IEntityId<TId>
    {
        IQueryable<T> AsQueryable();
        IEnumerable<T> AsEnumerable();
        Task<T?> RecuperarPorIdAsync(TId id);
        void Adicionar(T entity);
        void Atualizar(T entity);
        void Remover(T entity);
    }
}
