using Application.Dtos.Filtros.Tarefas;
using Application.Dtos.Tarefas;
using Application.Dtos.Tarefas.Subtarefas;
using Application.Utils.Paginacao;
using Domain.Entities;

namespace Application.Services.ServTarefas
{
    public interface IServTarefa
    {
        Task<TarefaResponse> AdicionarTarefa(CreateTarefaRequest dto);
        Task AtualizarTarefa(int id, UpdateTarefaRequest dto);
        Task RemoverTarefa(int id);
        Task<PaginacaoHelper<Tarefa>> ListarTarefas(TarefaFiltroRequest parametros);
        Task AtualizarPrioridade(int id, UpdatePrioridadeTarefaRequest dto);
        Task<TarefaResponse> ObterPorId(int id);
        Task<SubtarefaResponse> AdicionarSubtarefa(AdicionarSubtarefaRequest dto);
        Task AtualizarStatus(int id, UpdateStatusTarefaRequest dto);
        Task<HistoricoTarefaResponse> RecuperarHistoricoPorId(int id);
    }
}
