using Domain.Common;

namespace Application.Dtos.Tarefas
{
    public class TarefaResponse : IEntityId<int>
    {
        public int Id { get; set; }
    }
}
