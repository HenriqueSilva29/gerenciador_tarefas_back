using Application.Funcionalidades.Tarefas.Filtros;
using Application.Funcionalidades.Tarefas.Contratos.CasosDeUso;
using Application.Funcionalidades.UsuarioAutenticado.Servicos;
using Application.Utils.Filtro;
using Application.Utils.Ordenacao;
using Application.Utils.Paginacao;
using Application.Utils.Queryable;
using Domain.Entidades;
using Repository.Repositorios.Tarefas;

namespace Application.Funcionalidades.Tarefas.CasosDeUso
{
    public class ListarTarefas : IListarTarefasCasoDeUso
    {
        private readonly IRepTarefa _rep;
        private readonly IServicoUsuarioAutenticado _servUsuarioAutenticado;

        public ListarTarefas(
            IRepTarefa rep,
            IServicoUsuarioAutenticado servUsuarioAutenticado)
        {
            _rep = rep;
            _servUsuarioAutenticado = servUsuarioAutenticado;
        }

        public async Task<PaginacaoHelper<Tarefa>> Executar(TarefaFiltroRequisicao parametros)
        {
            var idUsuario = _servUsuarioAutenticado.ObterIdUsuarioLogado();

            var query = _rep.QueryPorUsuario(idUsuario);

            query = query.AplicarFiltros(parametros);
            query = query.AplicarOrdenacao(parametros);

            return await query.PaginarAsync(parametros.Pagina, parametros.QuantidadePorPagina);
        }
    }
}




