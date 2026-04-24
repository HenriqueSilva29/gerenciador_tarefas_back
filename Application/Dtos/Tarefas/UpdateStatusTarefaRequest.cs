using static Domain.Entities.Tarefa;

namespace Application.Dtos.Tarefas
{
    public class UpdateStatusTarefaRequest
    {
        public EnumStatusTarefa Status {  get; set; }
    }
}
