using Domain.Lembretes;
using Repository.ContextEFs;
using Repository.Repositorys.GuidRep;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositorys.LembreteRep
{
    public class RepLembrete : RepGuid<Lembrete>
    {
        public RepLembrete(ContextEF context) : base(context)
        {
            
        }
    }
}
