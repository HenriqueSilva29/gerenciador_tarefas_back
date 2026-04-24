namespace Domain.Exceptions
{
    public class ExceptionDomain : ExceptionBase
    {
        public override string Title => "Regra de negócio violada";
        public ExceptionDomain(string code, string message, int statusCode) : base(code, message, statusCode) 
        {

        }
    }
}
