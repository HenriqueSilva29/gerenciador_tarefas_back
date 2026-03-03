using static Domain.Entities.ToDoItem;

namespace Application.Dtos.ToDoItemDtos
{
    public class AtualizarPrioridadeToDoItemDto
    {
        public EnumPrioridadeToDoItem Prioridade { get; set; }
    }
}
