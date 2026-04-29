using Domain.Entidades;
using Repository.ModelosConsulta.Tarefas;

namespace Repository.Repositorios.Auditorias
{
    public interface IRepAuditoria : IRepositorio<Auditoria,int>
    {
        Task<List<HistoricoTarefaItemConsultaModelo>> RecuperarHistoricoDaTarefa(int idTarefa);
    }
}


