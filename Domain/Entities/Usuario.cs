using Domain.Common;

namespace Domain.Entities
{
    internal class Usuario : IEntityId<Guid>
    {
        public Guid Id { get; }
        public string Email { get; private set; }
        public string SenhaHash { get; private set; }
        public string Role { get; private set; }

        public Usuario(string email, string senhaHash, string role = "User")
        {
            Id = Guid.NewGuid();
            Email = email;
            SenhaHash = senhaHash;
            Role = role;
        }
    }
}
