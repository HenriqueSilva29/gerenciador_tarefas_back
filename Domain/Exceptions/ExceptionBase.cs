namespace Domain.Exceptions
{
    public abstract class ExceptionBase : Exception
    {
        public int StatusCode { get; }
        public string Code { get; }
        public abstract string Title { get; }
        public ExceptionBase(string code, string message, int statusCode) : base(message)
        {
            Code = code;
            StatusCode = statusCode;
        }
    }
}

