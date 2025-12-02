using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositorys.IntRep
{
    public interface IRepInt<T> : IRepository<T> where T : class
    {
        Task<T> RecuperarPorId(int id);
    }
}
