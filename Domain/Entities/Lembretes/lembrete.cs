using Domain.Common;
using Domain.Common.ValueObjects;
using Domain.Entities.ToDoItems;
using Domain.Exceptions;

namespace Domain.Entities.Lembretes
{
    public class Lembrete : IEntityGuid
    {
        public Guid Id { get; set; }
        public int CodigoToDoItem { get; set; }

        public ToDoItem ToDoItem { get; set; }

        public TimeSpan Antecedencia { get; set; }
        public bool FoiAgendado { get; set; }
        public UtcDateTime DataDeAgendamento { get; set; }
        public UtcDateTime? DataDeExecucaoDoAgendamento { get; set; }

        public string Texto { get; set; } = string.Empty;

        public LembreteStatus Status { get; set; }

        public enum LembreteStatus
        {
            Pendente = 0,
            Enviado = 1,
            Cancelado = 2
        }

        protected Lembrete() { }

        public Lembrete(ToDoItem toDoItem, TimeSpan antecedencia, string texto)
        {
            Id = Guid.NewGuid();
            ToDoItem = toDoItem;
            CodigoToDoItem = toDoItem.Id;
            Antecedencia = antecedencia;
            Texto = texto;

            Status = LembreteStatus.Pendente;
        }

        public void MarcarComoEnviado()
        {
            if (Status == LembreteStatus.Enviado)
                return;

            Status = LembreteStatus.Enviado;
            DataDeAgendamento = UtcDateTime.Now();
        }

        public void Cancelar()
        {
            if (Status == LembreteStatus.Enviado)
                throw new DomainException("LEMBRETE_ALREADY_SENT", "Lembrete já foi enviado.");

            Status = LembreteStatus.Cancelado;
        }

    }
}
