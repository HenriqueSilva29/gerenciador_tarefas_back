using Domain.Common;
using Domain.Common.ValueObjects;

namespace Domain.Entities
{
    public class Notificacao : IEntityId<int>
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

        public void MarcarComoLida()
        {
            this.Lida = true;
            this.DataLeitura = UtcDateTime.Now();
        }
    }

    public enum EnumTipoNotificacao
    {
        LembreteNotificado = 0,
        LembreteEntregue = 0,
        ResumoDiarioGerado = 1
    }
}
