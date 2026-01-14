using Domain.Common;

namespace Repository.Repositorys.IntRep
{
    public interface IRepInt<T> : IRepository<T> where T : class
    {
        Task<T?> RecuperarPorId(int id);
    }
}
