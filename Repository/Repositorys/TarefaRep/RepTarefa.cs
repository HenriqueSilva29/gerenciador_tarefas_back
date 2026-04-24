using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.ContextEFs;
using Repository.Repositorys;

namespace Repository.TarefaRep
{
    public class RepTarefa : Repository<Tarefa, int>, IRepTarefa
    {
        public RepTarefa(ContextEF context) : base(context)
        {
        }

        public async Task<List<Tarefa>> RecuperarSubtarefasVinculadasAhTarefa(int idTarefaPai)
        {
            return await AsQueryable()
                            .OrderBy(t => t.DataCriacao)
                            .Where(t => t.CodigoTarefaPai == idTarefaPai)
                            .ToListAsync();
        }
    }
}
