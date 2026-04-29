namespace Domain.Excecoes
{
    public class ExcecaoRepositorio : ExcecaoBase
    {
        public override string Title => "Erro ao executar o repositorio";
        public ExcecaoRepositorio(string code, string message, int statusCode)
        : base(code, message, statusCode) { }
    }
}

