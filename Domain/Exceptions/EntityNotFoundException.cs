namespace Domain.Exceptions
{
    public class EntityNotFoundException : DomainException
    {
        public EntityNotFoundException(string entity, string id)
        : base("ENTITY_NOT_FOUND", $"{entity} com id {id} não encontrada.")
        { }
    }
}
