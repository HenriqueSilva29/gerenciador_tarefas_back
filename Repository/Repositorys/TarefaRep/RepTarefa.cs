using Domain.Entities;
using Repository.ContextEFs;
using Repository.Repositorys;

namespace Repository.TarefaRep
{
    public class RepTarefa : Repository<Tarefa, int>, IRepTarefa
    {
        public RepTarefa(ContextEF context) : base(context)
        {
        }
    }
}
