using Domain.Common;
using Domain.Enums;
using Domain.Exceptions;
using Microsoft.AspNetCore.Http;

namespace Domain.Entities
{
    public class Lembrete : IEntityId<int>
    {
        public int Id { get; private set; }

        public int CodigoTarefa { get; private set; }
        public Tarefa Tarefa { get; private set; }

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
            int idTarefa,
            DateTimeOffset dataVencimento,
            int diasAntes,
            string texto)
        {
            CodigoTarefa = idTarefa;
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
                throw new ExceptionDomain(
                    EnumCodigosDeExcecao.LembreteJaEnviado,
                    "Lembrete já foi enviado",
                    StatusCodes.Status409Conflict);

            Status = LembreteStatus.Cancelado;
        }
    }
}
