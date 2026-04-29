using Application.Funcionalidades.Notificacoes.Filtros;
using Application.Funcionalidades.Notificacoes.Dtos;
using Application.Utils.Paginacao;

namespace Application.Funcionalidades.Notificacoes.Contratos.CasosDeUso
{
    public interface IListarNotificacoesCasoDeUso
    {
        Task<PaginacaoHelper<NotificacaoResposta>> ExecuteAsync(NotificacaoFiltroRequisicao filtro);
    }
}

