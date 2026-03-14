using static Domain.Entities.Tarefa;

namespace Application.Views
{
    public class TarefaView
    {
        public int CodigoTarefa { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public DateTimeOffset DataCriacao { get; set; }
        public DateTimeOffset? DataVencimento { get; set; }
        public EnumStatusTarefa Status { get; set; }
        public EnumPrioridadeTarefa Prioridade { get; set; }
        public EnumCategoriaTarefa Categoria { get; set; }

        public int? CodigoTarefaPai { get; set; }
        public List<int> SubTarefas { get; set; }
    }
}
