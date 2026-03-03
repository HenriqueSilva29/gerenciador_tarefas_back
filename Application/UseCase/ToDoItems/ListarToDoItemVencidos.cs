using Application.Interfaces.UseCases.ToDoItems;
using Application.Utils.Paginacao;
using Application.Utils.Queryable;
using Domain.Common.ValueObjects;
using Domain.Entities;
using Repository.ToDoItemRep;

namespace Application.UseCase.ToDoItems
{
    public class ListarToDoItemVencidos : IListarToDoItemVencidosUseCase
    {
        private readonly IRepToDoItem _rep;
        public ListarToDoItemVencidos(
            IRepToDoItem rep)
        {
            _rep = rep;
        }
        public async Task<PaginacaoHelper<ToDoItem>> Executar(int pagina, int quantidade)
        {
            var agoraUtc = UtcDateTime.Now();

            var query = _rep.AsQueryable()
                            .Where(t => t.DataVencimento.Value < agoraUtc.Value);

            return await query.PaginarAsync(pagina, quantidade);
        }
    }
}
