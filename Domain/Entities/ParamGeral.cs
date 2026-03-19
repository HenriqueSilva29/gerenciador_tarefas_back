using Domain.Common;

namespace Domain.Entities
{
    public class ParamGeral : IEntityId<int>
    {
        public int Id { get; set; }
        public bool AvisarVencimento { get; set; } = false;
        public int DiasAntesDoVencimento { get; set; } = 0;
    }
}
