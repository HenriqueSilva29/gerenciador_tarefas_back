using Application.Interfaces.UseCases.Tarefas;
using Application.Utils.Paginacao;
using Application.Utils.Queryable;
using Domain.Common.ValueObjects;
using Domain.Entities;
using Repository.TarefaRep;

namespace Application.UseCase.Tarefas
{
    public class ListarTarefasVencidas : IListarTarefasVencidasUseCase
    {
        private readonly IRepTarefa _rep;
        public ListarTarefasVencidas(
            IRepTarefa rep)
        {
            _rep = rep;
        }
        public async Task<PaginacaoHelper<Tarefa>> Executar(int pagina, int quantidade)
        {
            var agoraUtc = UtcDateTime.Now();

            var query = _rep.AsQueryable()
                            .Where(t => t.DataVencimento.Value < agoraUtc.Value);

            return await query.PaginarAsync(pagina, quantidade);
        }
    }
}
