using Application.Dtos.Filtros.Tarefas;
using Application.Dtos.Tarefas;
using Application.Dtos.Tarefas.Subtarefas;
using Application.Interfaces.UseCases.Tarefas;
using Application.Interfaces.UseCases.Tarefas.Subtarefas;
using Application.Services.ServTarefas;
using Application.Utils.Paginacao;
using Domain.Entities;

namespace Application.Services.TarefaServices
{
    public class ServTarefa : IServTarefa
    {
        private readonly IAdicionarTarefaUseCase _adicionarTarefa;
        private readonly IAtualizarPrioridadeTarefaUseCase _atualizarPrioridadeTarefa;
        private readonly IAtualizarTarefaUseCase _atualizarTarefa;
        private readonly IRemoverTarefaUseCase _removerTarefa;
        private readonly IListarTarefasUseCase _listarTarefa;
        private readonly IRecuperarTarefaPorIdUseCase _recuperarTarefaPorId;
        private readonly IAdicionarSubtarefaUseCase _adicionarSubtarefa;
        private readonly IAtualizarStatusTarefaUseCase _atualizarStatusTarefa;
        private readonly IRecuperarHistoricoTarefaUseCase _recuperarHistoricoTarefaUseCase;

        public ServTarefa(
            IAdicionarTarefaUseCase adicionarTarefa,
            IAtualizarPrioridadeTarefaUseCase atualizarPrioridadeTarefa,
            IRemoverTarefaUseCase removerTarefa,
            IListarTarefasUseCase listarTarefa,
            IAtualizarTarefaUseCase atualizarTarefa,
            IRecuperarTarefaPorIdUseCase recuperarTarefaPorId,
            IAdicionarSubtarefaUseCase adicionarSubtarefa,
            IAtualizarStatusTarefaUseCase atualizarStatusTarefa,
            IRecuperarHistoricoTarefaUseCase recuperarHistoricoTarefaUseCase) : base()
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

        public Task<TarefaResponse> AdicionarTarefa(CreateTarefaRequest dto)
        { 
            return _adicionarTarefa.Executar(dto); 
        }

        public Task AtualizarTarefa(int id, UpdateTarefaRequest dto)
            => _atualizarTarefa.Executar(id,dto);

        public Task AtualizarPrioridade(int id, UpdatePrioridadeTarefaRequest dto)
            => _atualizarPrioridadeTarefa.Executar(id, dto);

        public Task AtualizarStatus(int id, UpdateStatusTarefaRequest dto)
             => _atualizarStatusTarefa.Executar(id, dto);

        public  Task RemoverTarefa(int id)
            => _removerTarefa.Executar(id);

        public Task<PaginacaoHelper<Tarefa>> ListarTarefas(TarefaFiltroRequest parametros)
            => _listarTarefa.Executar(parametros);

        public Task<TarefaResponse> ObterPorId(int id)
        {
            return _recuperarTarefaPorId.Executar(id);
        }

        public Task<SubtarefaResponse> AdicionarSubtarefa(AdicionarSubtarefaRequest dto)
        {
            return _adicionarSubtarefa.Executar(dto);
        }


        public Task<HistoricoTarefaResponse> RecuperarHistoricoPorId(int id)
        {
            return _recuperarHistoricoTarefaUseCase.Executar(id);
        }

    }
}
