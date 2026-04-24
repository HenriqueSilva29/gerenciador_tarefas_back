using Application.Dtos.Filtros.Tarefas;
using Application.Interfaces.UseCases.ParamGerals;
using Application.Utils.Filtro;
using Application.Utils.Ordenacao;
using Application.Utils.Paginacao;
using Application.Utils.Queryable;
using Domain.Entities;
using Repository.Repositorys.ParamGeralRep;

namespace Application.UseCase.ParamGerals
{
    public class ListarParamGeralUseCase : IListarParamGeralUseCase
    {
        private readonly IRepParamGeral _rep;

        public ListarParamGeralUseCase(IRepParamGeral rep)
        {
            _rep = rep;
        }
        public Task<PaginacaoHelper<ParamGeral>> ExecutarAsync(ParamGeralFiltroRequest parametros)
        {
            var query = _rep.AsQueryable();

            query = query.AplicarFiltros(parametros);
            query = query.AplicarOrdenacao(parametros);

            return query.PaginarAsync(parametros.Pagina, parametros.QuantidadePorPagina);
        }
    }
}
