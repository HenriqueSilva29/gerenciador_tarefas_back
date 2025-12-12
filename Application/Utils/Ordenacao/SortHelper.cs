namespace Application.Utils.Ordenacao
{
    public static class SortHelper
    {
        public static IQueryable<T> AplicarOrdenacao<T>(
        this IQueryable<T> query,
        ISortHelper<T> sortDto)
        {
            var mapa = sortDto.ObterCamposOrdenaveis();

            if (!mapa.TryGetValue(sortDto.OrdenarPor, out var expressao))
                expressao = mapa.First().Value; 

            return sortDto.Direcao.ToLower() == "desc"
                ? query.OrderByDescending(expressao)
                : query.OrderBy(expressao);
        }
    }
}
