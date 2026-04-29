namespace Application.Funcionalidades.Notificacoes.Dtos
{
    public class NotificacaoTempoRealResposta
    {
        public int Id { get; set; }
        public int Tipo { get; set; }
        public string Titulo { get; set; } = default!;
        public string Mensagem { get; set; } = default!;
        public DateTimeOffset DataCriacao { get; set; }
        public bool Lida { get; set; }
    }
}

