using Application.Dtos.Filtros.Tarefas;
using Application.Dtos.ParamGerals;
using Application.Interfaces.UseCases.ParamGerals;
using Application.Utils.Paginacao;
using Domain.Entities;

namespace Application.Services.ServParamGerals
{
    public class ServParamGeral : IServParamGeral
    {
        private readonly IListarParamGeralUseCase _listarParamGeralUseCase;
        private readonly IAtualizarParamGeralUseCase _atualizarParamGeralUseCase;
        public ServParamGeral
        (
            IListarParamGeralUseCase listarParamGeralUseCase,
            IAtualizarParamGeralUseCase atualizarParamGeralUseCase
        )
        {
            _listarParamGeralUseCase = listarParamGeralUseCase;
            _atualizarParamGeralUseCase = atualizarParamGeralUseCase;
        }

        public Task<PaginacaoHelper<ParamGeral>> Listar(ParamGeralFiltroRequest filtro)
            =>  _listarParamGeralUseCase.ExecutarAsync(filtro);
        

        public Task Atualizar(int id, UpdateParamGeralRequest dto)
            => _atualizarParamGeralUseCase.ExecuteAsync(id, dto);
    }
}
