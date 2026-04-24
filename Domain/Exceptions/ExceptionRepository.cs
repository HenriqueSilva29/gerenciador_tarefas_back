namespace Domain.Exceptions
{
    public class ExceptionRepository : ExceptionBase
    {
        public override string Title => "Erro ao executar o repositorio";
        public ExceptionRepository(string code, string message, int statusCode)
        : base(code, message, statusCode) { }
    }
}
