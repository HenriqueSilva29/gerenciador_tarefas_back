using Application.Dtos.FiltroDtos;
using Application.Utils.Paginacao;
using Domain.Entities;

namespace Application.Interfaces.UseCases.Tarefas
{
    public interface IListarTarefasVencidasUseCase
    {
        public Task<PaginacaoHelper<Tarefa>> Executar(int pagina, int quantidade);
    }
}
