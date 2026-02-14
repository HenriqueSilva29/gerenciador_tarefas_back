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

        public DateTimeOffset DataDeVencimento { get; set; }
        public int DiasAntesDoVencimento { get; set; }
        public bool FoiAgendado { get; set; }
        public UtcDateTime DataDeAgendamento { get; set; }
        public UtcDateTime? DataDeExecucaoDoAgendamento { get; set; }

        public string Texto { get; set; } = string.Empty;

        public LembreteStatus Status { get; set; }

        public enum LembreteStatus
        {
            Pendente = 0,
            Executado = 1,
            Cancelado = 2
        }

        protected Lembrete() { }

        public Lembrete(ToDoItem toDoItem, DateTimeOffset dataDeVencimento, string texto)
        {
            Id = Guid.NewGuid();
            ToDoItem = toDoItem;
            CodigoToDoItem = toDoItem.Id;
            DataDeVencimento = dataDeVencimento;
            Texto = texto;

            Status = LembreteStatus.Pendente;
        }

        public void MarcarComoAgendado()
        {
            if (FoiAgendado)
                return;

            FoiAgendado = true;
            DataDeAgendamento = UtcDateTime.Now();
        }

        public void Executar()
        {
            if (Status == LembreteStatus.Executado)
                return;

            Status = LembreteStatus.Executado;
            DataDeExecucaoDoAgendamento = UtcDateTime.Now();
        }

        public void Cancelar()
        {
            if (Status == LembreteStatus.Executado)
                throw new DomainException("LEMBRETE_ALREADY_SENT", "Lembrete já foi enviado.");

            Status = LembreteStatus.Cancelado;
        }

    }
}
