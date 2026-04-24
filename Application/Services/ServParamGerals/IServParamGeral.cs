using Application.Dtos.Filtros.Tarefas;
using Application.Dtos.ParamGerals;
using Application.Utils.Paginacao;
using Domain.Entities;

namespace Application.Services.ServParamGerals
{
    public interface IServParamGeral
    {
        Task<PaginacaoHelper<ParamGeral>> Listar(ParamGeralFiltroRequest filtro);

        Task Atualizar(int id, UpdateParamGeralRequest filtro);
    }
}
