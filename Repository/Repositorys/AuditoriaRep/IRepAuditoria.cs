using Domain.Entities;
using Repository.QueryModels.Tarefas;

namespace Repository.Repositorys.AuditoriaRep
{
    public interface IRepAuditoria : IRepository<Auditoria,int>
    {
        Task<List<HistoricoTarefaItemQueryModel>> RecuperarHistoricoDaTarefa(int idTarefa);
    }
}
