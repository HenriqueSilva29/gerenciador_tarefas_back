using Domain.Common;
namespace Domain.Entities
{
    public class Usuario : IEntityId<int>
    {
        public int Id { get; }
        public string Email { get; set; }
        public string SenhaHash { get; private set; }
        public string Role { get; private set; }

        public Usuario(string email, string senhaHash, string role = "User")
        {
            Email = email;
            SenhaHash = senhaHash;
            Role = role;
        }

    }
}
