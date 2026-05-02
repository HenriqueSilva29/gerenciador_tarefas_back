namespace Application.Observabilidade
{
    public class CorrelationContext
    {
        public string CorrelationId { get; set; } = default!;
        public string? TraceId { get; set; }
        public string? TraceParent { get; set; }
        public string? TraceState { get; set; }
    }
}
