using Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using Repository.ContextosEF;
using Repository.ModelosConsulta.Tarefas;

namespace Repository.Repositorios.Auditorias
{
    public class RepAuditoria : Repositorio<Auditoria, int>, IRepAuditoria
    {
        public RepAuditoria(ContextEF context) : base(context){ }

        public async Task<List<HistoricoTarefaItemConsultaModelo>> RecuperarHistoricoDaTarefa(int idTarefa)
        {
            return await _context.Set<HistoricoTarefaItemConsultaModelo>()
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


