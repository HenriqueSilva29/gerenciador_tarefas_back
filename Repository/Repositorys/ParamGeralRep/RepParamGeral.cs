

using Domain.Entities;
using Repository.ContextEFs;

namespace Repository.Repositorys.ParamGeralRep
{
    public class RepParamGeral : Repository<ParamGeral,int>, IRepParamGeral 
    {
        public RepParamGeral(ContextEF context) : base(context){ }
    }
}
