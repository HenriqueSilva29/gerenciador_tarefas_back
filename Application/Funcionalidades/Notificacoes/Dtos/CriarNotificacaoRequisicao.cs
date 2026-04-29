using Domain.Entidades;

namespace Application.Funcionalidades.Notificacoes.Dtos
{
    public class CriarNotificacaoRequisicao
    {
        public int? CodigoUsuario { get; set; }
        public EnumTipoNotificacao Tipo { get; set; }
        public string Titulo { get; set; } = default!;
        public string Mensagem { get; set; } = default!;
    }
}


