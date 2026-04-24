using Domain.Entities;

namespace Application.Interfaces.UseCases.Lembretes
{
    public interface IEnviarLembretePorEmailUseCase
    {
        Task ExecuteAsync(Lembrete entity, string emailDestinatario);
    }
}
