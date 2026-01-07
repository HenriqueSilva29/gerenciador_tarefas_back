namespace Domain.Exceptions
{
    public class InvalidStateException : DomainException
    {
        public InvalidStateException(string code, string message)
        : base(code, message) { }
    }
}
