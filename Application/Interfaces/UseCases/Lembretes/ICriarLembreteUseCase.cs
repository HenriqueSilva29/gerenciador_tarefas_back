namespace Application.Interfaces.UseCases.Lembretes
{
    public interface ICriarLembreteUseCase
    {
        public Task CriarLembrete(int id, DateTimeOffset dataVencimento, int diasAntesDoVencimento);
    }
}
