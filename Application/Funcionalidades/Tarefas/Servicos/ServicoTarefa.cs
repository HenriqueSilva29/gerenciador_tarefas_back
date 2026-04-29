using Application.Funcionalidades.Tarefas.Filtros;
using Application.Funcionalidades.Tarefas.Dtos;
using Application.Funcionalidades.Tarefas.Dtos.Subtarefas;
using Application.Funcionalidades.Tarefas.Contratos.CasosDeUso;
using Application.Funcionalidades.Tarefas.Contratos.CasosDeUso.Subtarefas;
using Application.Funcionalidades.Tarefas.Servicos;
using Application.Utils.Paginacao;
using Domain.Entidades;

namespace Application.Funcionalidades.Tarefas.Servicos
{
    public class ServicoTarefa : IServicoTarefa
    {
        private readonly IAdicionarTarefaCasoDeUso _adicionarTarefa;
        private readonly IAtualizarPrioridadeTarefaCasoDeUso _atualizarPrioridadeTarefa;
        private readonly IAtualizarTarefaCasoDeUso _atualizarTarefa;
        private readonly IRemoverTarefaCasoDeUso _removerTarefa;
        private readonly IListarTarefasCasoDeUso _listarTarefa;
        private readonly IRecuperarTarefaPorIdCasoDeUso _recuperarTarefaPorId;
        private readonly IAdicionarSubtarefaCasoDeUso _adicionarSubtarefa;
        private readonly IAtualizarStatusTarefaCasoDeUso _atualizarStatusTarefa;
        private readonly IRecuperarHistoricoTarefaCasoDeUso _recuperarHistoricoTarefaUseCase;

        public ServicoTarefa(
            IAdicionarTarefaCasoDeUso adicionarTarefa,
            IAtualizarPrioridadeTarefaCasoDeUso atualizarPrioridadeTarefa,
            IRemoverTarefaCasoDeUso removerTarefa,
            IListarTarefasCasoDeUso listarTarefa,
            IAtualizarTarefaCasoDeUso atualizarTarefa,
            IRecuperarTarefaPorIdCasoDeUso recuperarTarefaPorId,
            IAdicionarSubtarefaCasoDeUso adicionarSubtarefa,
            IAtualizarStatusTarefaCasoDeUso atualizarStatusTarefa,
            IRecuperarHistoricoTarefaCasoDeUso recuperarHistoricoTarefaUseCase) : base()
        {
            _adicionarTarefa = adicionarTarefa;
            _atualizarPrioridadeTarefa = atualizarPrioridadeTarefa;
            _removerTarefa = removerTarefa;
            _listarTarefa = listarTarefa;
            _atualizarTarefa = atualizarTarefa;
            _recuperarTarefaPorId = recuperarTarefaPorId;
            _adicionarSubtarefa = adicionarSubtarefa;
            _atualizarStatusTarefa = atualizarStatusTarefa;
            _recuperarHistoricoTarefaUseCase = recuperarHistoricoTarefaUseCase;
        }

        public Task<TarefaResposta> AdicionarTarefa(CriarTarefaRequisicao dto)
        { 
            return _adicionarTarefa.Executar(dto); 
        }

        public Task AtualizarTarefa(int id, AtualizarTarefaRequisicao dto)
            => _atualizarTarefa.Executar(id,dto);

        public Task AtualizarPrioridade(int id, AtualizarPrioridadeTarefaRequisicao dto)
            => _atualizarPrioridadeTarefa.Executar(id, dto);

        public Task AtualizarStatus(int id, AtualizarStatusTarefaRequisicao dto)
             => _atualizarStatusTarefa.Executar(id, dto);

        public  Task RemoverTarefa(int id)
            => _removerTarefa.Executar(id);

        public Task<PaginacaoHelper<Tarefa>> ListarTarefas(TarefaFiltroRequisicao parametros)
            => _listarTarefa.Executar(parametros);

        public Task<TarefaResposta> ObterPorId(int id)
            => _recuperarTarefaPorId.Executar(id);

        public Task<SubtarefaResposta> AdicionarSubtarefa(AdicionarSubtarefaRequisicao dto)
            =>  _adicionarSubtarefa.Executar(dto);

        public Task<HistoricoTarefaResposta> RecuperarHistoricoPorId(int id)
            => _recuperarHistoricoTarefaUseCase.Executar(id);

    }
}



