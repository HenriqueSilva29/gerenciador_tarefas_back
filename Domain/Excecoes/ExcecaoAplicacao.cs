namespace Domain.Excecoes
{
    public class ExcecaoAplicacao : ExcecaoBase
    {
        public override string Title => "Erro na camada de aplicação";
        public ExcecaoAplicacao(string code, string message, int statusCode) : base(code, message, statusCode) { }

    }
}

