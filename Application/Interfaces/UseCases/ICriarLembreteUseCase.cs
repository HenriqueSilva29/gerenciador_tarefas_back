namespace Application.Interfaces.UseCases
{
    public interface ICriarLembreteUseCase
    {
        public Task CriarLembrete(int id, DateTimeOffset dataVencimento, int diasAntesDoVencimento);
    }
}
