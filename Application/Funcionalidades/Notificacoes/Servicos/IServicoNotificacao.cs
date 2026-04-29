using Application.Funcionalidades.Notificacoes.Filtros;
using Application.Funcionalidades.Notificacoes.Dtos;
using Application.Utils.Paginacao;

namespace Application.Funcionalidades.Notificacoes.Servicos
{
    public interface IServicoNotificacao
    {
        Task<PaginacaoHelper<NotificacaoResposta>> Listar(NotificacaoFiltroRequisicao filtro);
        Task<int> ContarNaoLidas();
        Task MarcarComoLida(int id);
        Task MarcarTodasComoLidas();
        Task Excluir(int id);
        Task ExcluirTodas();
    }
}

