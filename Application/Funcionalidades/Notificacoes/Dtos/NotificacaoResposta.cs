using Domain.Entidades;

namespace Application.Funcionalidades.Notificacoes.Dtos
{
    public class NotificacaoResposta
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


