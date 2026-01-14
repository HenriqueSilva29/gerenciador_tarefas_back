namespace Repository.Repositorys
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> AsQueryable();
        IEnumerable<T> AsEnumerable();
        Task Adicionar(T entity);
        Task Atualizar(T entity);
        Task Remover(T entity);
    }
}
