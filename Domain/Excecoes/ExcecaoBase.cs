namespace Domain.Excecoes
{
    public abstract class ExcecaoBase : Exception
    {
        public int StatusCode { get; }
        public string Code { get; }
        public abstract string Title { get; }
        public ExcecaoBase(string code, string message, int statusCode) : base(message)
        {
            Code = code;
            StatusCode = statusCode;
        }
    }
}


