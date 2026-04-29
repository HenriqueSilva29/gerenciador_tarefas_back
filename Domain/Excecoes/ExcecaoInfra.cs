namespace Domain.Excecoes
{
    public class ExcecaoInfra : ExcecaoBase
    {
        public override string Title => "Erro na camada de infra";
        public ExcecaoInfra(string code, string message, int statusCode)
        : base(code, message, statusCode)
        { }
    }
}

