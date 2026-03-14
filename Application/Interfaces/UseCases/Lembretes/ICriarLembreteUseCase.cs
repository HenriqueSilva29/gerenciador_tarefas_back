using Domain.Entities;

namespace Application.Interfaces.UseCases.Lembretes
{
    public interface ICriarLembreteUseCase
    {
        public void CriarLembrete(Tarefa entidade, DateTimeOffset dataVencimento, int diasAntesDoVencimento);
    }
}
