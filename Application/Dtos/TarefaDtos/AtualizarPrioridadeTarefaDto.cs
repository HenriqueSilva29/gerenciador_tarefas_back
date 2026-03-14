using static Domain.Entities.Tarefa;

namespace Application.Dtos.TarefaDtos
{
    public class AtualizarPrioridadeTarefaDto
    {
        public EnumPrioridadeTarefa Prioridade { get; set; }
    }
}
