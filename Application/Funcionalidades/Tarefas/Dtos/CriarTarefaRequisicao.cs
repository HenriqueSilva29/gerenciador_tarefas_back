using static Domain.Entidades.Tarefa;

namespace Application.Funcionalidades.Tarefas.Dtos
{
    public class CriarTarefaRequisicao
    {
        public string Titulo { get; set; }
        public string? Descricao { get; set; }
        public DateOnly DataTarefa { get; set; }
        public TimeOnly HoraInicio { get; set; }
        public TimeOnly HoraFim { get; set; }
        public EnumPrioridadeTarefa Prioridade { get; set; }
        public EnumCategoriaTarefa Categoria { get; set; }
    }
}


