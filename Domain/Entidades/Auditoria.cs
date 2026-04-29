using Domain.Comum;
using Domain.Comum.ObjetosDeValor;

namespace Domain.Entidades
{
    public class Auditoria : IEntidadeId<int>
    {
        public int Id { get; set; }
        public string Entidade { get; set; } = default!;
        public int IdEntidade { get; set; }
        public string Acao { get; set; } = default!;
        public string IdUsuario { get; set; } = default!;
        public UtcDateTime Data { get; set; }
        public string Alteracoes { get; set; } = default!;

    }


}

