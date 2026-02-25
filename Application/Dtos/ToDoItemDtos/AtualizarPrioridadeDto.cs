using static Domain.Entities.ToDoItem;

namespace Application.Dtos.ToDoItemDtos
{
    public class AtualizarPrioridadeDto
    {
        public EnumPrioridadeToDoItem Prioridade { get; set; }
    }
}
