using Application.Dtos.Filtros.Notificacoes;
using Application.Dtos.Notificacoes;
using Application.Interfaces.UseCases.Notificacoes;
using Application.Services.ServUsuarioAutenticados;
using Application.Utils.Filtro;
using Application.Utils.Ordenacao;
using Application.Utils.Paginacao;
using Application.Utils.Queryable;
using Microsoft.EntityFrameworkCore;
using Repository.Repositorys.NotificacaoRep;

namespace Application.UseCase.Notificacoes
{
    public class ListarNotificacoesUseCase : IListarNotificacoesUseCase
    {
        private readonly IRepNotificacao _repNotificacao;
        private readonly IServUsuarioAutenticado _usuarioAutenticado;

        public ListarNotificacoesUseCase
        (
            IRepNotificacao repNotificacao,
            IServUsuarioAutenticado usuarioAutenticado
        )
        {
            _repNotificacao = repNotificacao;
            _usuarioAutenticado = usuarioAutenticado;
        }

        public async Task<PaginacaoHelper<NotificacaoResponse>> ExecuteAsync(NotificacaoFiltroRequest filtro)
        {
            var idUsuario = _usuarioAutenticado.ObterIdUsuarioLogado();

            var query = _repNotificacao.AsQueryable()
                .AsNoTracking()
                .Where(n => n.CodigoUsuario == idUsuario);

            query = query.AplicarFiltros(filtro);
            query = query.AplicarOrdenacao(filtro);

            var pagina = await query.PaginarAsync(filtro.Pagina, filtro.QuantidadePorPagina);

            var notificacoes = pagina.Itens
                .Select(n => new NotificacaoResponse
                {
                    Id = n.Id,
                    Tipo = n.Tipo,
                    Titulo = n.Titulo,
                    Mensagem = n.Mensagem,
                    Lida = n.Lida,
                    DataCriacao = n.DataCriacao.Value,
                    DataLeitura = n.DataLeitura.HasValue ? n.DataLeitura.Value.Value : null
                });

            return new PaginacaoHelper<NotificacaoResponse>(
                notificacoes,
                pagina.PaginaAtual,
                pagina.QuantidadePorPagina,
                pagina.TotalItens);
        }
    }
}
