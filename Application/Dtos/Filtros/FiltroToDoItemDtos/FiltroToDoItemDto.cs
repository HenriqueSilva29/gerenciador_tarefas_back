using Domain.Enums.EnumToDoItem;

namespace Application.Dtos.Filtros.FiltroToDoItemDtos
{
    public class FiltroToDoItemDto : SortHelperDto
    {
        public int? CodigoToDoItem { get; set; }
        public string? Titulo { get; set; }
        public string? Descricao { get; set; }
        public DateTime? DataCriacao { get; set; }
        public DateTime? DataVencimento { get; set; }
        public EnumStatusToDoItem? Status { get; set; }
        public EnumPrioridadeToDoItem? Prioridade { get; set; }
        public EnumCategoriaToDoItem? Categoria { get; set; }

    }
}
