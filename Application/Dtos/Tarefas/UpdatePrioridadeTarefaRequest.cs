using static Domain.Entities.Tarefa;

namespace Application.Dtos.Tarefas
{
    public class UpdatePrioridadeTarefaRequest
    {
        public EnumPrioridadeTarefa Prioridade { get; set; }
    }
}
