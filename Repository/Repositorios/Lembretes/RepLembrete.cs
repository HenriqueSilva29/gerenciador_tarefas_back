using Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using Repository.ContextosEF;
using static Domain.Entidades.Lembrete;

namespace Repository.Repositorios.Lembretes
{
    public class RepLembrete : Repositorio<Lembrete, int>, IRepLembrete
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

        public async Task<Lembrete?> ObterComTarefaPorIdAsync(int id)
        {
            return await AsQueryable()
                .Include(l => l.Tarefa)
                .FirstOrDefaultAsync(l => l.Id == id);
        }
    }
}


