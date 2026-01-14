using Domain.Common;
using Domain.Common.ValueObjects;
using Domain.Entities.Lembretes;
using Domain.Exceptions;

namespace Domain.Entities.ToDoItems
{
    public class ToDoItem : IEntityInt
    {
        public ToDoItem()
        {
            Status = EnumStatusToDoItem.NaoIniciada;
            SubTarefas = new List<ToDoItem>();
            Lembretes = new List<Lembrete>();
        }

        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public UtcDateTime DataCriacao { get; set; }
        public UtcDateTime DataVencimento { get; set; }
        public EnumStatusToDoItem Status { get; set; }
        public EnumPrioridadeToDoItem Prioridade { get; set; }
        public EnumCategoriaToDoItem Categoria { get; set; }

        //autorreferente
        public int? CodigoToDoItemPai { get; set; }
        public ToDoItem? ToDoItemPai { get; set; }
        public List<ToDoItem> SubTarefas { get; set; }

        public List<Lembrete> Lembretes { get; set; }

        public enum EnumCategoriaToDoItem
        {
            Trabalho = 0,
            Pessoal = 1,
            Estudo = 2,
            Lazer = 3,
            Compras = 4,
            Casa = 5,
            Saude = 6,
            Outros = 7
        }

        public enum EnumPrioridadeToDoItem
        {
            Baixa = 0,
            Media = 1,
            Alta = 2
        }
        public enum EnumStatusToDoItem
        {
            NaoIniciada = 0,
            EmProgresso = 1,
            Concluida = 2,
            Cancelada = 3,
            Atrasada = 4,
            Pausada = 5,
            AguardandoRevisao = 6,
            Adiada = 7
        }

        public void CancelarTarefa()
        {
            Status = EnumStatusToDoItem.Cancelada;
        }

        public void DefinirPrioridade(EnumPrioridadeToDoItem prioridade)
        {
            Prioridade = prioridade;
        }

        public bool PossuiSubtarefaNaoConcluida()
        {
            return SubTarefas.Any(st => st.Status != EnumStatusToDoItem.Concluida);
        }

        public bool PodeConcluirTarefa()
        {
            return !PossuiSubtarefaNaoConcluida();
        }

        public void ConcluirTarefa()
        {
            if (!PodeConcluirTarefa())
                throw new DomainException(
                    "SUBTASK_NOT_COMPLETED",
                    "Não é possivel concluir tarefa pois existem subtarefas não concluídas");

            Status = EnumStatusToDoItem.Concluida;
        }

        public bool PossuiSubtarefaComPrioridadeMaior()
        {
            return SubTarefas.Any(st => st.Prioridade > Prioridade);
        }

        public bool ValidaPrioridadeParaSubtarefa()
        {
            if (PossuiSubtarefaComPrioridadeMaior())
                throw new DomainException(
                    "SUBTASK_NOT_COMPLETED",
                    "Subtarefa foi definida com prioridade maior que tarefa pai");

            return true;
        }
    }
}
