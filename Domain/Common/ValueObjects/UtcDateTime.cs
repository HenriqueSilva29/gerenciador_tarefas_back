
namespace Domain.Common.ValueObjects
{
    public readonly struct UtcDateTime
    {
        public DateTimeOffset Value { get;  }

        public UtcDateTime(DateTimeOffset value)
        {
            if (value.Offset != TimeSpan.Zero)
                throw new ArgumentException("Must be UTC");

            Value = value;
        }

        public UtcDateTime Subtract(TimeSpan time)
        {
            return new UtcDateTime(Value - time);
        }

        public static UtcDateTime From(DateTimeOffset value)
           => new UtcDateTime(value.ToUniversalTime());

        public static UtcDateTime From(DateOnly date, TimeOnly time)
        {
            var dateTime = date.ToDateTime(time);
            var utc = DateTime.SpecifyKind(dateTime, DateTimeKind.Utc);

            return new UtcDateTime(new DateTimeOffset(utc));
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
