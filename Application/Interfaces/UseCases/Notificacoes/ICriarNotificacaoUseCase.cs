using Application.Dtos.Notificacoes;
using Domain.Entities;

namespace Application.Interfaces.UseCases.Notificacoes
{
    public interface ICriarNotificacaoUseCase
    {
        Task<Notificacao> ExecuteAsync(CriarNotificacaoRequest dto);
    }
}
