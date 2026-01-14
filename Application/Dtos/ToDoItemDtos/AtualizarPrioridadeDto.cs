using static Domain.Entities.ToDoItems.ToDoItem;

namespace Application.Dtos.ToDoItemDtos
{
    public class AtualizarPrioridadeDto
    {
        public EnumPrioridadeToDoItem Prioridade { get; set; }
    }
}
