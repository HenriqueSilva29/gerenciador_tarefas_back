using Application.Funcionalidades.ParamGerais.Dtos;
using Application.Funcionalidades.ParamGerais.Contratos.CasosDeUso;
using Domain.Entidades;

namespace Application.Funcionalidades.ParamGerais.Servicos
{
    public class ServicoParamGeral : IServicoParamGeral
    {
        private readonly IListarParamGeralCasoDeUso _listarParamGeralUseCase;
        private readonly IAtualizarParamGeralCasoDeUso _atualizarParamGeralUseCase;

        public ServicoParamGeral(
            IListarParamGeralCasoDeUso listarParamGeralUseCase,
            IAtualizarParamGeralCasoDeUso atualizarParamGeralUseCase)
        {
            _listarParamGeralUseCase = listarParamGeralUseCase;
            _atualizarParamGeralUseCase = atualizarParamGeralUseCase;
        }

        public Task<ParamGeral> Obter()
            => _listarParamGeralUseCase.ExecutarAsync();

        public Task Atualizar(AtualizarParamGeralRequisicao dto)
            => _atualizarParamGeralUseCase.ExecuteAsync(dto);
    }
}


