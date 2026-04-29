using Application.Funcionalidades.Tarefas.Filtros;
using Application.Funcionalidades.Tarefas.Dtos;
using Application.Funcionalidades.Tarefas.Dtos.Subtarefas;
using Application.Utils.Paginacao;
using Domain.Entidades;

namespace Application.Funcionalidades.Tarefas.Servicos
{
    public interface IServicoTarefa
    {
        Task<TarefaResposta> AdicionarTarefa(CriarTarefaRequisicao dto);
        Task AtualizarTarefa(int id, AtualizarTarefaRequisicao dto);
        Task RemoverTarefa(int id);
        Task<PaginacaoHelper<Tarefa>> ListarTarefas(TarefaFiltroRequisicao parametros);
        Task AtualizarPrioridade(int id, AtualizarPrioridadeTarefaRequisicao dto);
        Task<TarefaResposta> ObterPorId(int id);
        Task<SubtarefaResposta> AdicionarSubtarefa(AdicionarSubtarefaRequisicao dto);
        Task AtualizarStatus(int id, AtualizarStatusTarefaRequisicao dto);
        Task<HistoricoTarefaResposta> RecuperarHistoricoPorId(int id);
    }
}



