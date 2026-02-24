using Domain.Common;
using Domain.Entities.ToDoItems;
using Domain.Exceptions;

namespace Domain.Entities.Lembretes
{
    public class Lembrete : IEntityInt
    {
        public int Id { get; private set; }

        public int CodigoToDoItem { get; private set; }
        public ToDoItem ToDoItem { get; private set; }

        public DateTimeOffset DataVencimento { get; private set; }
        public DateTimeOffset DataDisparo { get; private set; }

        public string Texto { get; private set; }

        public LembreteStatus Status { get; private set; }

        public enum LembreteStatus
        {
            Pendente = 0,
            Executado = 1,
            Cancelado = 2
        }

        protected Lembrete() { }

        public Lembrete(
            int idtoDoItem,
            DateTimeOffset dataVencimento,
            int diasAntes,
            string texto)
        {
            CodigoToDoItem = idtoDoItem;
            DataVencimento = dataVencimento;
            DataDisparo = dataVencimento.AddDays(-diasAntes);
            Texto = texto;
            Status = LembreteStatus.Pendente;
        }

        public void Executar()
        {
            if (Status != LembreteStatus.Pendente)
                return;

            Status = LembreteStatus.Executado;
        }

        public void Cancelar()
        {
            if (Status == LembreteStatus.Executado)
                throw new DomainException(
                    "LEMBRETE_ALREADY_SENT",
                    "Lembrete já foi enviado.");

            Status = LembreteStatus.Cancelado;
        }
    }
}
