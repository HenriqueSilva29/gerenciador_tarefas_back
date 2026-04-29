using Domain.Comum;
using Domain.Enumeradores;
using Domain.Excecoes;
using Microsoft.AspNetCore.Http;

namespace Domain.Entidades
{
    public class Lembrete : IEntidadeId<int>
    {
        public int Id { get; private set; }

        public int CodigoTarefa { get; private set; }
        public Tarefa Tarefa { get;  set; }
        public DateTimeOffset DataDisparo { get; private set; }

        public string Descricao { get; set; }

        public EnumLembreteStatus Status { get; set; }

        public enum EnumLembreteStatus
        {
            Pendente = 0,
            Executado = 1,
            Cancelado = 2
        }

        protected Lembrete() { }

        public Lembrete(
            int idTarefa,
            DateTimeOffset dataDisparo)
        {
            CodigoTarefa = idTarefa;
            DataDisparo = dataDisparo;
        }

        public void Executar()
        {
            if (Status != EnumLembreteStatus.Pendente)
                return;

            Status = EnumLembreteStatus.Executado;
        }

        public void Cancelar()
        {
            if (Status == EnumLembreteStatus.Executado)
                throw new ExcecaoDominio(
                    EnumCodigosDeExcecao.LembreteJaEnviado,
                    "Lembrete já foi enviado",
                    StatusCodes.Status409Conflict);

            Status = EnumLembreteStatus.Cancelado;
        }
    }
}

