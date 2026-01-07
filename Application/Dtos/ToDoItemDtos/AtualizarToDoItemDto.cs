using static Domain.ToDoItems.ToDoItem;

namespace Application.Dtos.ToDoItemDtos
{
    public class AtualizarToDoItemDto
    {
        public string Titulo { get; set; }
        public string? Descricao { get; set; }
        public DateTime? DataVencimento { get; set; }
        public EnumPrioridadeToDoItem Prioridade { get; set; }
        public EnumCategoriaToDoItem Categoria { get; set; }
        public EnumStatusToDoItem Status { get; set; }
    }
}
