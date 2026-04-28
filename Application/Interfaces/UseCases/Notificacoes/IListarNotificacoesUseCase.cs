using Application.Dtos.Filtros.Notificacoes;
using Application.Dtos.Notificacoes;
using Application.Utils.Paginacao;

namespace Application.Interfaces.UseCases.Notificacoes
{
    public interface IListarNotificacoesUseCase
    {
        Task<PaginacaoHelper<NotificacaoResponse>> ExecuteAsync(NotificacaoFiltroRequest filtro);
    }
}
