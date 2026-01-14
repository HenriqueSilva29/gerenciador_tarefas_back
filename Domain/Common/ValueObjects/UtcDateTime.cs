namespace Domain.Common.ValueObjects
{
    public readonly struct UtcDateTime
    {
        public DateTimeOffset Value { get;  }

        public UtcDateTime(DateTimeOffset value)
        {
            Value = value;
        }

        public static UtcDateTime Now()
            => new(DateTimeOffset.UtcNow);
        
        public static implicit operator DateTimeOffset(UtcDateTime utc) 
            => utc.Value;

        public static implicit operator UtcDateTime(DateTimeOffset value)
        => new(value);

        public override string ToString()
        => Value.ToString("O");
    }
}
