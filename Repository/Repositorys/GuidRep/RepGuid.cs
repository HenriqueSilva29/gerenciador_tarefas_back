using Domain.Common;
using Microsoft.EntityFrameworkCore;
using Repository.ContextEFs;

namespace Repository.Repositorys.GuidRep
{
    public class RepGuid<T> : Repository<T>, IRepGuid<T> where T : class, IEntityGuid
    {
        private readonly ContextEF _context;
        public RepGuid(ContextEF context) : base(context) 
        {
            _context = context;
        }

        public async Task<T?> RecuperarPorGuid(Guid id)  
        {
            return await _context.Set<T>().FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
