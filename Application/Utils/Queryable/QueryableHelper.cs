using Application.Utils.Paginacao;
using Microsoft.EntityFrameworkCore;

namespace Application.Utils.Queryable
{
    public static class QueryableHelper
    {
        public static async Task<PaginacaoHelper<T>> PaginarAsync<T>(
        this IQueryable<T> query,
        int pagina,
        int quantidadePorPagina)
        {
            if (pagina <= 0) pagina = 1;
            if (quantidadePorPagina <= 0) quantidadePorPagina = 10;

            var totalItens = await query.CountAsync();

            var itens = await query
                .Skip((pagina - 1) * quantidadePorPagina)
                .Take(quantidadePorPagina)
                .ToListAsync();

            return new PaginacaoHelper<T>(itens, pagina, quantidadePorPagina, totalItens);
        }
    }
}
