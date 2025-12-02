using Microsoft.EntityFrameworkCore;
using Repository.ContextEFs;

namespace Repository.Repositorys.GuidRep
{
    public class RepGuid<T> : Repository<T>, IRepGuid<T> where T : class
    {
        private readonly ContextEF _context;
        private readonly DbSet<T> _dbSet;
        public RepGuid(ContextEF context) : base(context) 
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<T> RecuperarPorGuid(Guid id) 
            => await _dbSet.FirstOrDefaultAsync(x => EF.Property<Guid>(x, "Id") == id);
    }
}
