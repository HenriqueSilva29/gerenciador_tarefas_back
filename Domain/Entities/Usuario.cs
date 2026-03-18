using Domain.Common;
namespace Domain.Entities
{
    public class Usuario : IEntityId<int>
    {
        public int Id { get; }
        public string Nome { get; set; }
        public string SenhaHash { get; private set; }
        public string Role { get; private set; }

        public Usuario(string nome, string senhaHash, string role = "User")
        {
            Nome = nome;
            SenhaHash = senhaHash;
            Role = role;
        }

    }
}
