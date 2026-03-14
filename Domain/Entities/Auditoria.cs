using Domain.Common;
using Domain.Common.ValueObjects;

namespace Domain.Entities
{
    public class Auditoria : IEntityId<int>
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
