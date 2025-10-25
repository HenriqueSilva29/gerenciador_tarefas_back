using Domain.Enums.EnumToDoItem;

namespace Application.Dtos.ToDoItemDtos
{
    public class ToDoItemDto : SortHelperDto
    {
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataVencimento { get; set; }
        public EnumStatusToDoItem Status { get; set; }
        public EnumPrioridadeToDoItem Prioridade { get; set; }
        public EnumCategoriaToDoItem Categoria { get; set; }
    }
}
