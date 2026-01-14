using Domain.Entities.Lembretes;
using Repository.ContextEFs;
using Repository.Repositorys.GuidRep;

namespace Repository.Repositorys.LembreteRep
{
    public class RepLembrete : RepGuid<Lembrete>, IRepLembrete
    {
        public RepLembrete(ContextEF context) : base(context)
        {
            
        }
    }
}
