using Domain.Enums.EnumToDoItem;
using Domain.Lembretes;

namespace Domain.ToDoItems
{
    public class ToDoItem
    {

        public ToDoItem()
        {
            Status = EnumStatusToDoItem.NaoIniciada;
            SubTarefas = new List<ToDoItem>();
            this.Lembretes = new List<Lembrete>();
        }

        public int CodigoToDoItem { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataVencimento { get; set; }
        public EnumStatusToDoItem Status { get; set; }
        public EnumPrioridadeToDoItem Prioridade { get; set; }
        public EnumCategoriaToDoItem Categoria { get; set; }

        //autorreferente
        public int? CodigoToDoItemPai { get; set; }
        public ToDoItem? ToDoItemPai { get; set; }
        public List<ToDoItem> SubTarefas { get; set; }

        public List<Lembrete> Lembretes { get; set; }

        public void ConcluirTarefa()
        {
            Status = EnumStatusToDoItem.Concluida;
        }

        public void CancelarTarefa()
        {
            Status = EnumStatusToDoItem.Cancelada;
        }

        public bool EstaVencida()
        {
            return DataVencimento.HasValue && DataVencimento.Value < DateTime.Now;
        }

        public bool EstaNoPrazo()
        {
            return DataVencimento.HasValue && DataVencimento.Value > DateTime.Now;
        }

        public void DefinirPrioridade(EnumPrioridadeToDoItem prioridade)
        {
            Prioridade = prioridade;
        }

    }
}
