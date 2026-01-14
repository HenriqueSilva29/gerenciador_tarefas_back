using Domain.Common;

namespace Repository.Repositorys.GuidRep
{
    public interface IRepGuid<T> : IRepository<T> where T : class
    {
        public Task<T?> RecuperarPorGuid(Guid id);
    }
}
