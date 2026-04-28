using Application.Dtos.Filtros.Notificacoes;
using Application.Dtos.Notificacoes;
using Application.Utils.Paginacao;

namespace Application.Services.ServNotificacoes
{
    public interface IServNotificacao
    {
        Task<PaginacaoHelper<NotificacaoResponse>> Listar(NotificacaoFiltroRequest filtro);
        Task<int> ContarNaoLidas();
        Task MarcarComoLida(int id);
        Task MarcarTodasComoLidas();
        Task Excluir(int id);
        Task ExcluirTodas();
    }
}
