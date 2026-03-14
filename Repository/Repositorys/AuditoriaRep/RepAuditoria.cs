using Domain.Entities;
using Repository.ContextEFs;

namespace Repository.Repositorys.AuditoriaRep
{
    public class RepAuditoria : Repository<Auditoria, int>, IRepAuditoria
    {
        public RepAuditoria(ContextEF context) : base(context){ }


    }
}
