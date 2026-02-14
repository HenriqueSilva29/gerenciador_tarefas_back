using static Domain.Entities.ToDoItems.ToDoItem;

namespace Application.Dtos.ToDoItemDtos
{
    public class AdicionarToDoItemDto
    {
        public string Titulo { get; set; }
        public string? Descricao { get; set; }
        public DateTimeOffset DataVencimento { get; set; }
        public EnumStatusToDoItem Status { get; set; }
        public EnumPrioridadeToDoItem Prioridade { get; set; }
        public EnumCategoriaToDoItem Categoria { get; set; }

        public bool AvisarQueVaiVencer { get; set; }
        public int DiasAntesDoVencimento { get; set; }
    }
}
