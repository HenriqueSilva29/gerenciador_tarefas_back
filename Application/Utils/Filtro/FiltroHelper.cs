using Application.Dtos.Filtros;

namespace Application.Utils.Filtro
{
    public static class FiltroHelper 
    {
        public static IQueryable<T> AplicarFiltros<T>(
            this IQueryable<T> query,
            ITarefaFiltroRequest<T> filtroDto)
        {
            var filtros = filtroDto.ObterFiltros();

            foreach (var f in filtros.Values)
            {
                query = query.Where(f);
            }

            return query;
        }
    }
}
