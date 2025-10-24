using Domain.Enums;

namespace Domain.ToDoItem
{
    public class ToDoItem
    {

        public ToDoItem()
        {
            Status = EnumStatusToDoItem.NaoIniciada;
        }

        public int CodigoToDoItem { get; set; }
        public int Codigo { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataVencimento { get; set; }
        public EnumStatusToDoItem Status { get; set; }
        public EnumPrioridadeToDoItem Prioridade { get; set; }
        public EnumCategoriaToDoItem Categoria { get; set; }

        private void ConcluirTarefa()
        {
            Status = EnumStatusToDoItem.Concluida;
        }

        private void CancelarTarefa()
        {
            Status = EnumStatusToDoItem.Cancelada;
        }

    }
}
