namespace Application.Messaging
{
    public class MessageEnvelope
    {
        public string Type { get; set; }
        public string CorrelationId { get; set; }
        public string? TraceParent { get; set; }
        public string? TraceState { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Payload { get; set; }
    }
}
