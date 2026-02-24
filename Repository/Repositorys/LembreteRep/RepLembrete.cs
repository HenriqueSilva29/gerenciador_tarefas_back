using Domain.Entities.Lembretes;
using Microsoft.EntityFrameworkCore;
using Repository.ContextEFs;
using Repository.Repositorys.GuidRep;
using Repository.Repositorys.IntRep;

namespace Repository.Repositorys.LembreteRep
{
    public class RepLembrete : RepInt<Lembrete>, IRepLembrete
    {
        public RepLembrete(ContextEF context) : base(context)
        {
        }
        public async Task<List<Lembrete>> ObterPendentesParaDisparo(
            DateTimeOffset agora)
        {
            return await AsQueryable()
                .Where(l =>
                    l.Status == Lembrete.LembreteStatus.Pendente &&
                    l.DataDisparo <= agora)
                .ToListAsync();
        }
    }
}
