using Application.Dtos.Filtros.Tarefas;
using Application.Utils.Paginacao;
using Domain.Entities;

namespace Application.Interfaces.UseCases.ParamGerals
{
    public interface IListarParamGeralUseCase
    {
        Task<PaginacaoHelper<ParamGeral>> ExecutarAsync(ParamGeralFiltroRequest filtro);
    }
}
