namespace Application.Observabilidade
{
    public class CorrelationContextAccessor : ICorrelationContextAccessor
    {
        private static readonly AsyncLocal<CorrelationContext?> Current = new();

        public CorrelationContext? Context
        {
            get => Current.Value;
            set => Current.Value = value;
        }
    }
}
