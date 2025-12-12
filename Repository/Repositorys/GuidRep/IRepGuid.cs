namespace Repository.Repositorys.GuidRep
{
    public interface IRepGuid<T> : IRepository<T> where T : class
    {
        Task<T> RecuperarPorGuid(Guid id);
    }
}
