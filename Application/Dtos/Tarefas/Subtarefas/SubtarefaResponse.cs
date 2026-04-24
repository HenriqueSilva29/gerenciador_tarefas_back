using Domain.Common;

namespace Application.Dtos.Tarefas.Subtarefas
{
    public class SubtarefaResponse : IEntityId<int>
    {
        public int Id { get; set; }
    }
}
