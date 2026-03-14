using Application.Dtos.FiltroDtos;
using Application.Interfaces.UseCases.Tarefas;
using Application.Utils.Filtro;
using Application.Utils.Ordenacao;
using Application.Utils.Paginacao;
using Application.Utils.Queryable;
using Domain.Entities;
using Repository.TarefaRep;

namespace Application.UseCase.Tarefas
{
    public class ListarTarefa : IListarTarefaUseCase
    {
        private readonly IRepTarefa _rep;
        public ListarTarefa(
            IRepTarefa rep)
        {
            _rep = rep;
        }
        public async Task<PaginacaoHelper<Tarefa>> Executar(FiltroTarefaDto parametros)
        {
            var query = _rep.AsQueryable();

            query = query.AplicarFiltros(parametros);
            query = query.AplicarOrdenacao(parametros);

            return await query.PaginarAsync(parametros.Pagina, parametros.QuantidadePorPagina);
        }
    }
}
