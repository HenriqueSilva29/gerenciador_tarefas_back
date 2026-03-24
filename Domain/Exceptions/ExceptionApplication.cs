namespace Domain.Exceptions
{
    public class ExceptionApplication : ExceptionBase
    {
        public override string Title => "Erro na camada de aplicação";
        public ExceptionApplication(string code, string message, int statusCode) : base(code, message, statusCode) { }

    }
}
