using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.ContextEFs;
using static Domain.Entities.Lembrete;

namespace Repository.Repositorys.LembreteRep
{
    public class RepLembrete : Repository<Lembrete, int>, IRepLembrete
    {
        public RepLembrete(ContextEF context) : base(context)
        {
        }

        public async Task<List<Lembrete>> ObterPendentesParaDisparo(
            DateTimeOffset agora)
        {
            return await AsQueryable()
                .Where(l =>
                    l.Status == EnumLembreteStatus.Pendente &&
                    l.DataDisparo <= agora)
                .ToListAsync();
        }
    }
}
