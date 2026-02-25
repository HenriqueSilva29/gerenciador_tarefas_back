namespace Domain.Common
{
    public interface IEntityId<T>
    {
        T Id { get; }
    }
}
