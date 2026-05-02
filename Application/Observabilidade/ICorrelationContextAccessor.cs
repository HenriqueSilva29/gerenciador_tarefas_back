namespace Application.Observabilidade
{
    public interface ICorrelationContextAccessor
    {
        CorrelationContext? Context { get; set; }
    }
}
