namespace Domain.Excecoes
{
    public class ExcecaoDominio : ExcecaoBase
    {
        public override string Title => "Regra de negócio violada";
        public ExcecaoDominio(string code, string message, int statusCode) : base(code, message, statusCode) 
        {

        }
    }
}

