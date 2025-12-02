using Microsoft.EntityFrameworkCore;
using Repository.ContextEFs;
using Repository.Repositorys.GuidRep;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositorys.IntRep
{
    public class RepInt<T> : Repository<T>, IRepInt<T> where T : class
    {

        private readonly ContextEF _context;
        private readonly DbSet<T> _dbSet;

        public RepInt(ContextEF context) : base(context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<T> RecuperarPorId(int id)
            =>  await _dbSet.FindAsync(id);

    }
}
