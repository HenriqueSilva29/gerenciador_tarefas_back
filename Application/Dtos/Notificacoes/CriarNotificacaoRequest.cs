using Domain.Entities;

namespace Application.Dtos.Notificacoes
{
    public class CriarNotificacaoRequest
    {
        public int? CodigoUsuario { get; set; }
        public EnumTipoNotificacao Tipo { get; set; }
        public string Titulo { get; set; } = default!;
        public string Mensagem { get; set; } = default!;
    }
}
