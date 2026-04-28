using Domain.Entities;

namespace Application.Dtos.Notificacoes
{
    public class NotificacaoResponse
    {
        public int Id { get; set; }
        public EnumTipoNotificacao Tipo { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Mensagem { get; set; } = string.Empty;
        public bool Lida { get; set; }
        public DateTimeOffset DataCriacao { get; set; }
        public DateTimeOffset? DataLeitura { get; set; }
    }
}
