namespace Domain.Exceptions
{
    public class ExceptionInfra : ExceptionBase
    {
        public override string Title => "Erro na camada de infra";
        public ExceptionInfra(string code, string message, int statusCode)
        : base(code, message, statusCode)
        { }
    }
}
