using Application.Dtos.FiltroDtos;
using Application.Interfaces.UseCases.ToDoItems;
using Application.Utils.Filtro;
using Application.Utils.Ordenacao;
using Application.Utils.Paginacao;
using Application.Utils.Queryable;
using Domain.Entities;
using Repository.ToDoItemRep;

namespace Application.UseCase.ToDoItems
{
    public class ListarToDoItem : IListarToDoItemUseCase
    {
        private readonly IRepToDoItem _rep;
        public ListarToDoItem(
            IRepToDoItem rep)
        {
            _rep = rep;
        }
        public async Task<PaginacaoHelper<ToDoItem>> Executar(FiltroToDoItemDto parametros)
        {
            var query = _rep.AsQueryable();

            query = query.AplicarFiltros(parametros);
            query = query.AplicarOrdenacao(parametros);

            return await query.PaginarAsync(parametros.Pagina, parametros.QuantidadePorPagina);
        }
    }
}
