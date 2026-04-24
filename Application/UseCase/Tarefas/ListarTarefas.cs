using Application.Dtos.Filtros.Tarefas;
using Application.Interfaces.UseCases.Tarefas;
using Application.Utils.Filtro;
using Application.Utils.Ordenacao;
using Application.Utils.Paginacao;
using Application.Utils.Queryable;
using Domain.Entities;
using Repository.TarefaRep;

namespace Application.UseCase.Tarefas
{
    public class ListarTarefas : IListarTarefasUseCase
    {
        private readonly IRepTarefa _rep;
        public ListarTarefas(
            IRepTarefa rep)
        {
            _rep = rep;
        }
        public async Task<PaginacaoHelper<Tarefa>> Executar(TarefaFiltroRequest parametros)
        {
            var query = _rep.AsQueryable();

            query = query.AplicarFiltros(parametros);
            query = query.AplicarOrdenacao(parametros);

            return await query.PaginarAsync(parametros.Pagina, parametros.QuantidadePorPagina);
        }
    }
}
