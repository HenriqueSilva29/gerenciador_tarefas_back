using Application.Interfaces.UseCases.Autenticacaos;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Infra.Autenticacao
{
    public class HashSenha : IHashSenhaUseCase
    {
        private readonly PasswordHasher<Usuario> _hasher = new();
        public string Executar(string senha)
        {
            return _hasher.HashPassword(null, senha);
        }
    }
}
