using Domain.Comum;
using Domain.Comum.ObjetosDeValor;

namespace Domain.Entidades
{
    public class Notificacao : IEntidadeId<int>
    {
        public int Id { get; set; }
        public int? CodigoUsuario { get; set; }
        public Usuario? Usuario { get; set; }
        public EnumTipoNotificacao Tipo { get; set; }
        public string Titulo { get; set; } = default!;
        public string Mensagem { get; set; } = default!;
        public bool Lida { get; set; } = false;
        public UtcDateTime DataCriacao { get; set; }
        public UtcDateTime? DataLeitura { get; set; }

        public static Notificacao Criar(
            int? codigoUsuario,
            EnumTipoNotificacao tipo,
            string titulo,
            string mensagem)
        {
            return new Notificacao
            {
                CodigoUsuario = codigoUsuario,
                Tipo = tipo,
                Titulo = titulo,
                Mensagem = mensagem,
                DataCriacao = UtcDateTime.Now(),
                Lida = false,
                DataLeitura = null
            };
        }

        public void MarcarComoLida()
        {
            this.Lida = true;
            this.DataLeitura = UtcDateTime.Now();
        }

        public void MarcarComoNaoLida()
        {
            Lida = false;
            DataLeitura = null;
        }
    }

    public enum EnumTipoNotificacao
    {
        LembreteNotificado = 0,
        LembreteEntregue = 0,
        ResumoDiarioGerado = 1
    }
}

