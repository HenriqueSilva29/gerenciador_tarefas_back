using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.ContextEFs;
using Repository.QueryModels.Tarefas;

namespace Repository.Repositorys.AuditoriaRep
{
    public class RepAuditoria : Repository<Auditoria, int>, IRepAuditoria
    {
        public RepAuditoria(ContextEF context) : base(context){ }

        public async Task<List<HistoricoTarefaItemQueryModel>> RecuperarHistoricoDaTarefa(int idTarefa)
        {
            return await _context.Set<HistoricoTarefaItemQueryModel>()
                .FromSqlInterpolated($@"
                    SELECT *
                    FROM dbo.fn_obter_historico_tarefa({idTarefa})
                    ORDER BY dataocorrencia ASC, idauditoria ASC
                ")
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
