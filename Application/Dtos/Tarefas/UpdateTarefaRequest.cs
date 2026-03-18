using static Domain.Entities.Tarefa;

namespace Application.Dtos.Tarefas
{
    public class UpdateTarefaRequest
    {
        public string Titulo { get; set; }
        public string? Descricao { get; set; }
        public DateTimeOffset DataVencimento { get; set; }
        public EnumPrioridadeTarefa Prioridade { get; set; }
        public EnumCategoriaTarefa Categoria { get; set; }
        public EnumStatusTarefa Status { get; set; }
    }
}
