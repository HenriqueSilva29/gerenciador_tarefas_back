using System.Diagnostics;

namespace Application.Observabilidade
{
    public static class ObservabilidadeFonte
    {
        public const string Nome = "OrganizaAi";
        public static readonly ActivitySource ActivitySource = new(Nome);
    }
}
