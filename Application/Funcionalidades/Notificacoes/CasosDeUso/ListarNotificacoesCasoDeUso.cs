using Application.Funcionalidades.Notificacoes.Filtros;
using Application.Funcionalidades.Notificacoes.Dtos;
using Application.Funcionalidades.Notificacoes.Contratos.CasosDeUso;
using Application.Funcionalidades.UsuarioAutenticado.Servicos;
using Application.Utils.Filtro;
using Application.Utils.Ordenacao;
using Application.Utils.Paginacao;
using Application.Utils.Queryable;
using Microsoft.EntityFrameworkCore;
using Repository.Repositorios.Notificacoes;

namespace Application.Funcionalidades.Notificacoes.CasosDeUso
{
    public class ListarNotificacoesCasoDeUso : IListarNotificacoesCasoDeUso
    {
        private readonly IRepNotificacao _repNotificacao;
        private readonly IServicoUsuarioAutenticado _usuarioAutenticado;

        public ListarNotificacoesCasoDeUso
        (
            IRepNotificacao repNotificacao,
            IServicoUsuarioAutenticado usuarioAutenticado
        )
        {
            _repNotificacao = repNotificacao;
            _usuarioAutenticado = usuarioAutenticado;
        }

        public async Task<PaginacaoHelper<NotificacaoResposta>> ExecuteAsync(NotificacaoFiltroRequisicao filtro)
        {
            var idUsuario = _usuarioAutenticado.ObterIdUsuarioLogado();

            var query = _repNotificacao.QueryPorUsuario(idUsuario)
                .AsNoTracking();

            query = query.AplicarFiltros(filtro);
            query = query.AplicarOrdenacao(filtro);

            var pagina = await query.PaginarAsync(filtro.Pagina, filtro.QuantidadePorPagina);

            var notificacoes = pagina.Itens
                .Select(n => new NotificacaoResposta
                {
                    Id = n.Id,
                    Tipo = n.Tipo,
                    Titulo = n.Titulo,
                    Mensagem = n.Mensagem,
                    Lida = n.Lida,
                    DataCriacao = n.DataCriacao.Value,
                    DataLeitura = n.DataLeitura.HasValue ? n.DataLeitura.Value.Value : null
                });

            return new PaginacaoHelper<NotificacaoResposta>(
                notificacoes,
                pagina.PaginaAtual,
                pagina.QuantidadePorPagina,
                pagina.TotalItens);
        }
    }
}



