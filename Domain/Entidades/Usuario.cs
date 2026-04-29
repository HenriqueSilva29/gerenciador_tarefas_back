using Domain.Comum;
namespace Domain.Entidades
{
    public class Usuario : IEntidadeId<int>
    {
        public int Id { get; }
        public string Email { get; set; }
        public string SenhaHash { get; private set; }
        public string Role { get; private set; }
        public string? Nome { get; set; } = null;
        public ParamGeral? ParamGeral { get; set; }

        public Usuario(string email, string senhaHash, string role = "User")
        {
            Email = email;
            SenhaHash = senhaHash;
            Role = role;
        }

        public void AtualizarNomeDoUsuario(string nome)
        {
            this.Nome = nome;
        }

    }
}

