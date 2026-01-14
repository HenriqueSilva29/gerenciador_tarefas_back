using Domain.Common;
using Microsoft.EntityFrameworkCore;
using Repository.ContextEFs;

namespace Repository.Repositorys.IntRep
{
    public class RepInt<T> : Repository<T>, IRepInt<T> where T : class, IEntityInt
    {

        private readonly ContextEF _context;

        public RepInt(ContextEF context) : base(context)
        {
            _context = context;
        }

        public async Task<T?> RecuperarPorId(int id)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(x => x.Id == id);
        }

    }
}
