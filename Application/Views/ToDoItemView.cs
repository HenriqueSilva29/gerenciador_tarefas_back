using static Domain.ToDoItems.ToDoItem;

namespace Application.Views
{
    public class ToDoItemView
    {
        public int CodigoToDoItem { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataVencimento { get; set; }
        public EnumStatusToDoItem Status { get; set; }
        public EnumPrioridadeToDoItem Prioridade { get; set; }
        public EnumCategoriaToDoItem Categoria { get; set; }

        public int? CodigoToDoItemPai { get; set; }
        public List<int> SubTarefas { get; set; }
    }
}
